using BookStore.Books.Entity;
using BookStore.Books.Models;

namespace BookStore.Books.Interface
{
    public interface IBook
    {
        BooksEntity AddBook(BookModel book);    
        BooksEntity GetBookById(int id);
        IEnumerable<BooksEntity> GetAllBooks();
        BooksEntity UpdateBook(int id, BookModel book);
        bool DeleteBook(int id);    

    }
}
