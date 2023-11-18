using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord of The Rings", 0, 0, 0)]
        [InlineData("Lord of The Rings", 0, 1, 0)]
        [InlineData("Lord of The Rings", 0, 1, 1)]
        [InlineData("Lord of The Rings", 0, 0, 1)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 100, 0, 0)]
        [InlineData("", 100, 1, 0)]
        [InlineData("", 100, 0, 1)]
        [InlineData("", 100, 1, 1)]
        [InlineData(" ", 100, 1, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;

            command.Model = new UpdateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = genreId,
                AuthorId = authorId
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = "Updated Hobbit Title",
                PageCount = 250,
                PublishDate = DateTime.Now.Date,
                GenreId = 2,
                AuthorId = 2
            };

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 10;
            command.Model = new UpdateBookModel()
            {
                Title = "Updated Hobbit Title",
                PageCount = 250,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 2,
                AuthorId = 2
            };
            
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}