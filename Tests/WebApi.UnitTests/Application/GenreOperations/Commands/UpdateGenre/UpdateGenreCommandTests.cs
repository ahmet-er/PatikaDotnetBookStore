using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyHaveNotGenreInDb_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 312;
        
            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");
        }
        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeUpdated()
        {
            // arrange
            var initialGenre = new Genre()
            {
                Name = "Test Genre 1"
            };
            _context.Genres.Add(initialGenre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = initialGenre.Id;

            var updatedGenreModel = new UpdateGenreModel()
            {
                Name = "Updated Test Genre 1"
            };

            command.Model = updatedGenreModel;

            // act & assert
            FluentActions.Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}