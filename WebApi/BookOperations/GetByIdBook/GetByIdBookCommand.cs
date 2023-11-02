using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;
using static WebApi.BookOperations.GetByIdBook.GetByIdBookCommand;

namespace WebApi.BookOperations.GetByIdBook
{
    public class GetByIdBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetByIdBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetByIdBookViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(book => book.Id == id).SingleOrDefault();
             GetByIdBookViewModel vm = _mapper.Map<GetByIdBookViewModel>(book); // new GetByIdBookViewModel();
            // vm.Title = book.Title;
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            // vm.Genre = ((GenreEnum)book.GenreId).ToString();
            // vm.PageCount = book.PageCount;
            
            return vm;
        }

        public class GetByIdBookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}