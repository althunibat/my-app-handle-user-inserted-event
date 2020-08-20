using FluentValidation;

namespace Godwit.HandleUserInsertedEvent.Model.Validators {
    public class HasuraEventValidator : AbstractValidator<HasuraEvent> {
        public HasuraEventValidator() {
            RuleFor(x => x.Table.Name).Must(p => p == "users");
            RuleFor(x => x.Event.Operation).Must(p => p == "INSERT");
            RuleFor(x => x.Event.Data.NewValue.IsConfirmed).Must(p => p == false);
            RuleFor(x => x.Event.Data.NewValue.Email).NotEmpty();
            RuleFor(x => x.Event.Data.NewValue.UserName).NotEmpty();
        }
    }
}