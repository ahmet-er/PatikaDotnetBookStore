using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Entities;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
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
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookCommand.CreateBookModel(){
                Title =  title, 
                PageCount = pageCount, 
                PublishDate = DateTime.Now.Date.AddYears(-1), 
                GenreId = genreId, 
                AuthorId = authorId
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
             command.Model = new CreateBookCommand.CreateBookModel(){
                Title =  "Lord of the rings", 
                PageCount = 100, 
                PublishDate = DateTime.Now.Date, 
                GenreId = 1, 
                AuthorId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
             command.Model = new CreateBookCommand.CreateBookModel(){
                Title =  "Lord of the rings", 
                PageCount = 100, 
                PublishDate = DateTime.Now.Date.AddYears(-2), 
                GenreId = 1, 
                AuthorId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}