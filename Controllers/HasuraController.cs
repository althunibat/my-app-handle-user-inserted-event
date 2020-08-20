using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Godwit.HandleUserInsertedEvent.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using User = Godwit.Common.Data.Model.User;

namespace Godwit.HandleUserInsertedEvent.Controllers {
    [ApiController]
    [Route("")]
    public class HasuraController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly UserManager<User> _manager;
        private readonly ISendGridClient _sendGridClient;
        private readonly IValidator<HasuraEvent> _validator;

        public HasuraController(IValidator<HasuraEvent> validator,
            ILogger<HasuraController> logger, ISendGridClient sendGridClient, IConfiguration configuration,
            UserManager<User> manager) {
            _validator = validator;
            _logger = logger;
            _sendGridClient = sendGridClient;
            _configuration = configuration;
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HasuraEvent model) {
            _logger.LogInformation($"Call Started by {model.Event.Session.UserId} having role {model.Event.Session.Role}");
            var validation = _validator.Validate(model);
            if (!validation.IsValid)
            {
                _logger.LogWarning("request validation failed!");
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage)
                );
            }

            try {
                var user = await _manager.FindByNameAsync(model.Event.Data.NewValue.UserName).ConfigureAwait(false);
                var token = await _manager.GenerateEmailConfirmationTokenAsync(user);
                var message =
                    $"Dear {user.FirstName} {user.LastName},<br>to complete registration with Keto App, please use the token below <br> <span><strong>{token}</strong></span><br> Regards,<br>Keto App";

                var msg = MailHelper.CreateSingleEmail(new EmailAddress(_configuration["FROM_EMAIL"], "Keto App"),
                    new EmailAddress(user.Email, model.Event.Data.NewValue.Name),
                    "Thank you for registering with Keto App", "", message);
                var response = await _sendGridClient.SendEmailAsync(msg).ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
                    _logger.LogWarning($"Unable to send Email: {response.StatusCode}");

                return StatusCode((int)response.StatusCode);
            }
            catch (Exception e) {
                _logger.LogError(new EventId(1001, "Exception"), e, "Unable to Save Data!");
                return Problem("Unable to Send Email!, An Exception Occur!");
            }
        }
    }
}