using FluentValidation;
using LibraryAPI.Dtos.Category;

namespace LibraryAPI.Validators
{
    public class CategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CategoryValidator()
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

    public class CategoryUpdateModelValidator : AbstractValidator<CategoryDto>
    {
        public CategoryUpdateModelValidator()
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
