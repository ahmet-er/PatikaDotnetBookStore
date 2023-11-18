using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotAuthorInDb_InvalidOperation_ShouldBeReturn()
        {
            // arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 214;

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek yazar bulunamadı!");
        }
        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeUpdated()
        {
            // arrange
            var initialAuthor = new Author()
            {
                FirstName = "Test",
                LastName = "Author 1",
                BirthDate = DateTime.Now.Date.AddYears(-42)
            };
            _context.Authors.Add(initialAuthor);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = initialAuthor.Id;

            var updatedAuthorModel = new UpdateAuthorModel()
            {
                FirstName = "Updated",
                LastName = "Author 1",
                BirthDate = DateTime.Now.Date.AddYears(-72)
            };
            command.Model = updatedAuthorModel;

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}