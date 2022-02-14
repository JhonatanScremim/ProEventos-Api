using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain.Identity;

namespace ProEventos.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUserByNameAsync(string name);
    }
}