using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorFirstNameAndLastNameAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var author = new Author()
            {
                FirstName = "Test",
                LastName = "Author",
                BirthDate = DateTime.Now.Date.AddYears(-67)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel() 
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            // act&assert
            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                FirstName = "New",
                LastName = "Author",
                BirthDate = DateTime.Now.Date.AddYears(-88)
            };
            command.Model = model;
        
            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
        
            // assert
            var author = _context.Authors.SingleOrDefault(author => author.FirstName == model.FirstName && author.LastName == model.LastName);
            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}