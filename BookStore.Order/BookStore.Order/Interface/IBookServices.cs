using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IBookServices
    {
        Task<BookEntity> GetBookById(int id);

    }
}
