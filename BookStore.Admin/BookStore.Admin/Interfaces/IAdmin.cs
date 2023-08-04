using BookStore.Admin.Entity;
using BookStore.Admin.Models;

namespace BookStore.Admin.Interfaces
{
    public interface IAdmin
    {
        AdminEntity RegisterAdmin(AdminModel adminModel);
        string AdminLogin(string email, string password);

    }
}
