using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
         private readonly BookStoreDbContext _context;
         public DeleteAuthorCommandTests(CommonTestFixture testFixture)
         {
            _context = testFixture.Context;
         }
         [Fact]
        public void WhenAlreadyHaveNotAuthorInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 99;

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı!");
        }
        [Fact]
        public void WhenIfAuthorHaveAnyBook_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu yazarın kitabı olduğu için silemezsiniz");
        }
        [Fact]
        public void WhenValidAuthorIdIsGiven_Book_SouldBeDeleted()
        {
            //arrange
            var author = new Author ()
            {
                FirstName = "Delete",
                LastName = "Author 234",
                BirthDate = DateTime.Now.Date.AddYears(-632),
                Books = null
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = author.Id;

            //act
            command.Handle();

            //assert
            var deletedAuthor = _context.Authors.Find(author.Id);
            deletedAuthor.Should().BeNull();
        }
    }
}