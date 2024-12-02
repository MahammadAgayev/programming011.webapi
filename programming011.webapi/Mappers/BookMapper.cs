using programming011.webapi.Entitites;
using programming011.webapi.Models;

namespace programming011.webapi.Mappers
{
    public class BookMapper
    {
        public static BookModel MapModel(Book b)
        {
            return new BookModel
            {
                Id = b.Id,
                AuthorName = b.AuthorName,
                ReleaseYear = b.ReleaseYear,
                Title = b.Title,
            };
        }

        public static Book MapBook(BookModel model)
        {
            return new Book
            {
                Id = model.Id,
                AuthorName = model.AuthorName,
                ReleaseYear = model.ReleaseYear,
                Title = model.Title,
            };
        }
    }
}
