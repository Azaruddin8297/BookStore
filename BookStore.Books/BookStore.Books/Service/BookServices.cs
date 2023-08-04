using BookStore.Books.Entity;
using BookStore.Books.Interface;
using BookStore.Books.Models;

namespace BookStore.Books.Service
{
    public class BookServices : IBook
    {
        private readonly BooksContext _db;
        public BookServices(BooksContext db)
        {
              _db = db;
        }
        public BooksEntity AddBook(BookModel newBook)
        {
            BooksEntity book = new BooksEntity();
            book.BookName = newBook.BookName;
            book.AuthorName = newBook.AuthorName;
            book.Description = newBook.Description;
            book.Ratings = newBook.Ratings;
            book.Reviews = newBook.Reviews;
            book.DiscountedPrice = newBook.DiscountedPrice;
            book.OriginalPrice = newBook.OriginalPrice;
            book.Quantity = newBook.Quantity;
            _db.Books.Add(book);
            _db.SaveChanges();
            return book;

        }

        public bool DeleteBook(int id)
        {
            BooksEntity book = _db.Books.FirstOrDefault(x => x.BookId == id);
            if (book != null)
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<BooksEntity> GetAllBooks()
        {
            IEnumerable<BooksEntity> book = _db.Books;
            if(book != null)
            {
                return book;
            }
            else
            {
                return null;
            }
        }

        public BooksEntity GetBookById(int id)
        {
            BooksEntity book = _db.Books.FirstOrDefault(x => x.BookId == id);
            if(book != null)
            {
                return book;
            }
            else
            {
                return null;
            }
           
        }

        public BooksEntity UpdateBook(int id, BookModel updateModel)
        {
            BooksEntity book = _db.Books.FirstOrDefault(x => x.BookId == id);
            if(book != null)
            {
                book.BookName = updateModel.BookName;
                book.AuthorName = updateModel.AuthorName;
                book.Description = updateModel.Description;
                book.Ratings = updateModel.Ratings;
                book.Reviews = updateModel.Reviews;
                book.DiscountedPrice = updateModel.DiscountedPrice;
                book.OriginalPrice = updateModel.OriginalPrice;
                book.Quantity = updateModel.Quantity;

                _db.Books.Update(book);
                _db.SaveChanges();
                return book;

            }
            else
            {
                return null;
            }
        }
    }
}
