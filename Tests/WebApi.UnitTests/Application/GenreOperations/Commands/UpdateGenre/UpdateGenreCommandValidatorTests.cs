using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            
            command.Model = new UpdateGenreModel()
            {
                Name = "12"
            };
        
            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
        
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 12;
            command.Model = new UpdateGenreModel()
            {
                Name = "Updated Genre"
            };
        
            // act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
        
            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}