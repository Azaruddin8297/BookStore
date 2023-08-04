using BookStore.Books.Entity;
using BookStore.Books.Interface;
using BookStore.Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookController : ControllerBase
    {
        private readonly IBook _book;
        public ResponseEntity response;
        public bookController(IBook book)
        {
              _book = book;
            response = new ResponseEntity();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddBook")]
        public ResponseEntity AddBook(BookModel newBook)
        {
            try
            {
                BooksEntity book = _book.AddBook(newBook);
                if (book != null)
                {
                    response.Data = book;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("EditBook")]
        public ResponseEntity UpdateBook(int id, BookModel newBook)
        {
            BooksEntity book = _book.UpdateBook(id,newBook);
            if (book != null)
            {
                response.Data = book;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }
            return response;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteBook")]
        public ResponseEntity DeleteBook(int id)
        {
            bool book = _book.DeleteBook(id);
            if (book != null)
            {
                response.Data = book;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }
            return response;
        }
        [HttpGet]
        [Route("GetBookById")]
        public ResponseEntity GetBookById(int id)
        {
            BooksEntity book = _book.GetBookById(id);
            if (book != null)
            {
                response.Data = book;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }
            return response;
        }
        [HttpGet]
        [Route("GetAllBooks")]
        public ResponseEntity GetAllBooks()
        {
           IEnumerable<BooksEntity> book = _book.GetAllBooks();
            if (book.Any())
            {
                response.Data = book;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "No Books are found";
            }
            return response;
        }
    }
}
