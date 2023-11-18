using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotGenreInDb_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 929;

            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");
        }
        [Fact]
        public void WhenValidBookIdIsGiven_Book_SouldBeDeleted()
        {
            //arrange
            var genre = new Genre()
            { Name = "Delete Genre" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genre.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var deletedGenre = _context.Genres.Find(genre.Id);
            deletedGenre.Should().BeNull();
        }
    }
}