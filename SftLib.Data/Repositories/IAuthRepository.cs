using SftLib.Data.Domain.Models;
using System.Threading.Tasks;

namespace SftLib.Data.Repositories
{
    public interface IAuthRepository
    {
        Task Register(User user, string password);
        Task<User> Login(string username, string password);
    }
}
