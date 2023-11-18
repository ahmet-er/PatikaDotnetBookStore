using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAuthorIdIsValid_Author_ShouldNotBeReturnError()
        {
            // Given
            var author = new Author()
            {
                FirstName = "Test Name",
                LastName = "Last Name",
                BirthDate = DateTime.Now.Date.AddYears(-25)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = author.Id;

            // When
            var result = query.Handle();

            // Then
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Test Name");
            result.LastName.Should().Be("Last Name");
        }
    }
}