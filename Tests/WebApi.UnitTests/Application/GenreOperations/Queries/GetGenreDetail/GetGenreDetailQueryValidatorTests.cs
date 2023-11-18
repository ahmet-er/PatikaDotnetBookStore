using System.Configuration;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int genreId)
        {
            // arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = genreId;
        
            // act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);
        
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 1;
        
            // When
            GetGenreDetailQueryValidator  validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);
        
            // Then
            result.Errors.Count.Should().Be(0);
        }
    }
}