using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyHaveNotBookInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 99;

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı.");
        }
        [Fact]
        public void WhenValidBookIdIsGiven_Book_SouldBeDeleted()
        {
            //arrange
            var book = new Book() { Title = "DeleteTest", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-5), GenreId = 1, AuthorId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = book.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var deletedBook = _context.Books.Find(book.Id);
            deletedBook.Should().BeNull();
        }
    }
}