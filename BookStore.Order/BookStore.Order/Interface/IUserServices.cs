using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IUserServices
    {
         Task<UserEntity> GetUser(string jwtToken);

    }
}
