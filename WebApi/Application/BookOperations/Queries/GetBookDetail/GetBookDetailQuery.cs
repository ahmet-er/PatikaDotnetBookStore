using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetBookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(book => book.Id == BookId).SingleOrDefault();
             GetBookDetailViewModel vm = _mapper.Map<GetBookDetailViewModel>(book);

            return vm;
        }

        public class GetBookDetailViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}