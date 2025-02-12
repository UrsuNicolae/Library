using FluentValidation;
using LibraryAPI.Dtos.Author;

namespace LibraryAPI.Validators
{
    public class AuthorModelValidator : AbstractValidator<CreateAuthorDto>
    {
        public AuthorModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty!")
                .NotNull()
                .WithMessage("Name should not be null!")
                .MaximumLength(50)
                .WithMessage("Name maximum length is 50!");
        }
    }

    public class AuthorUpdateModelValidator : AbstractValidator<AuthorDto>
    {
        public AuthorUpdateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty!")
                .NotNull()
                .WithMessage("Name should not be null!")
                .MaximumLength(50)
                .WithMessage("Name maximum length is 50!");

            RuleFor(x => x.Id)
                .Must(x => x > 0)
                .WithMessage("Invalid Id!");
        }
    }
}
