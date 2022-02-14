using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain.Identity;
using ProEventos.Repository.Context;
using ProEventos.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUserByNameAsync(string name)
        {
            return await _context.Users.AsNoTracking().Where(x => x.FirstName.ToLower().Contains(name)).ToListAsync();
        }
    }
}