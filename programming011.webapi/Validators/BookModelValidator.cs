using FluentValidation;
using programming011.webapi.Models;

namespace programming011.webapi.Validators
{
    public class BookModelValidator : AbstractValidator<BookModel>
    {
        public BookModelValidator() 
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required")
                .NotEmpty().WithMessage("Title could not be empty")
                .Length(3, 25).WithMessage("Title length should be in range 3 and 25")
                .Must(CheckUnique).WithMessage("Title already exists");

            RuleFor(x => x.ReleaseYear)
                .GreaterThan(1799).WithMessage("Release year should be greater than 1799")
                .LessThan(DateTime.Now.Year + 1).WithMessage("Release year should be less than current year");

            RuleFor(x => x.AuthorName)
                .NotNull().WithMessage("Author name is required")
                .NotEmpty().WithMessage("Author name could not be empty")
                .Length(3, 25).WithMessage("Author name length should be in range 3 and 25")
                .Matches(@"^[A-Z][a-z]*$").WithMessage("Author name should start with an uppercase letter and contain only lowercase letters after the first letter.");
        }

        private bool CheckUnique(string bookName)
        {
            // check books table for this book name

            return true;
        }
    }
}
