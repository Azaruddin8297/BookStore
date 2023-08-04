using BookStore.User.Entity;
using BookStore.User.Model;

namespace BookStore.User.Interface
{
    public interface IUserServices
    {
        UserEntity Register(UserModel user);
        string Login(string email, string password);
        bool ForgetPassword(string email);
        bool ResetPassword(string newPassword,string confirmPassword,string email);
        UserEntity GetUser(int userid);
    }
}
