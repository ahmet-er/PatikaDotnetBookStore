using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGenreIdIsValid_Genre_ShouldNotBeReturnError()
        {
            // arrange
            var genre = new Genre()
            {
                Name = "Test Genre"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = genre.Id;
        
            // act
            var result = query.Handle();
        
            // asset
            result.Should().NotBeNull();
            result.Name.Should().Be("Test Genre");
        }
    }
}