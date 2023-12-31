using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int bookId)
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;

            // When
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);
        
            // Then
            result.Errors.Count.Should().Be(0);
        }
    }
}