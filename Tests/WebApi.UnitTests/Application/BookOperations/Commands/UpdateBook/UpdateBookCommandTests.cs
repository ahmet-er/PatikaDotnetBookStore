using System.Configuration;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotBookInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 99;

            //act&assert
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı.");
        }
        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeUpdated()
        {
            //arrange
            var initialBook = new Book()
            {
                Title = "Hobbit",
                PageCount = 200,
                PublishDate = DateTime.Now.Date.AddYears(-4),
                GenreId = 1,
                AuthorId = 1,
            };
            _context.Books.Add(initialBook);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = initialBook.Id;

            var updatedBookModel = new UpdateBookModel()
            {
                Title = "Updated Hobbit Title",
                PageCount = 250,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 2,
                AuthorId = 2
            };

            command.Model = updatedBookModel;

            //act&assert
            FluentActions.Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}