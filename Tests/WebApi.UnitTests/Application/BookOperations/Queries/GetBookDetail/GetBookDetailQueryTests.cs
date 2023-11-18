using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenBookIdIsValid_Book_ShouldNotBeReturnError()
        {
            //arrange
            var book = new Book()
            {
                Title = "Hobbit",
                PageCount = 200,
                PublishDate = DateTime.Now.Date.AddYears(-4),
                GenreId = 1,
                AuthorId = 2
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = book.Id;

            //act
            var result = query.Handle();

            //assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Hobbit");
            result.PageCount.Should().Be(200);
            result.Genre.Should().NotBeNull();
            result.Author.Should().NotBeNull();
        }
    }
}