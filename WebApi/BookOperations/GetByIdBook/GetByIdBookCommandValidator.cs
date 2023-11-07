using FluentValidation;

namespace WebApi.BookOperations.GetByIdBook
{
    public class GetByIdBookCommandValidator : AbstractValidator<GetByIdBookCommand>
    {
        public GetByIdBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}