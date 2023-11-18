using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "test")]
        [InlineData(" ", "test")]
        [InlineData("test", "")]
        [InlineData("test", " ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBReturnErrors(string firstName, string lastName)
        {
            // arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;

            command.Model = new UpdateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = DateTime.Now.AddYears(-53)
            };
        
            // act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
        
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "testt",
                LastName = "validator",
                BirthDate = DateTime.Now.Date
            };

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
         [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 10;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "Updated",
                LastName = "Author",
                BirthDate = DateTime.Now.AddYears(-53)
            };
            
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}