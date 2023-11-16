using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "Eric",
                        LastName = "Ries",
                        BirthDate = DateTime.Parse("01.01.1970")
                    },
                    new Author
                    {
                        FirstName = "Charlotte Perkins",
                        LastName = "Gilman",
                        BirthDate = DateTime.Parse("02.02.1888")
                    },
                    new Author
                    {
                        FirstName = "Frank",
                        LastName = "Herbert",
                        BirthDate = DateTime.Parse("03.03.1940")
                    }
                );
        }
    }
}