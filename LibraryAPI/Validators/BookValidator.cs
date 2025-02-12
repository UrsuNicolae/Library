using FluentValidation;
using LibraryAPI.Dtos.Book;

namespace LibraryAPI.Validators
{
    public class BookValidator : AbstractValidator<CreateBookDto>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title should not be empty!")
                .NotNull()
                .WithMessage("Title should not be null!")
                .MaximumLength(50)
                .WithMessage("Title maximum length is 50!");

            RuleFor(x => x.Price)
                .Must(x => x > 0)
                .WithMessage("Price should be greater than 0!");

            RuleFor(x => x.AuthorId)
                .Must(x => x > 0)
                .WithMessage("Invalid Author Id!");

            RuleFor(x => x.CategoryId)
                .Must(x => x > 0)
                .WithMessage("Invalid Category Id!");
        }
    }

    public class BookUpdateValidator : AbstractValidator<BookDto>
    {
        public BookUpdateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title should not be empty!")
                .NotNull()
                .WithMessage("Title should not be null!")
                .MaximumLength(50)
                .WithMessage("Title maximum length is 50!");

            RuleFor(x => x.Price)
                .Must(x => x > 0)
                .WithMessage("Price should be greater than 0!");

            RuleFor(x => x.AuthorId)
                .Must(x => x > 0)
                .WithMessage("Invalid Author Id!");

            RuleFor(x => x.CategoryId)
                .Must(x => x > 0)
                .WithMessage("Invalid Category Id!");

            RuleFor(x => x.Id)
                .Must(x => x > 0)
                .WithMessage("Invalid Id!");
        }
    }
}
