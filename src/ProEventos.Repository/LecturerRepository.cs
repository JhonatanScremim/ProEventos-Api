using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Repository.Context;
using ProEventos.Domain;
using ProEventos.Repository.Interfaces;

namespace ProEventos.Repository
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly DataContext _context;
        public LecturerRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Lecturer[]> GetAllLecturersAsync(bool includeEvents)
        {
            var query = _context.Lecturer.Include(x => x.SocialNetworks);
            
            if(includeEvents){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Event);
            }
            
            return await query.OrderBy(x => x.Id).ToArrayAsync();
        }

        public async Task<Lecturer[]> GetAllLecturersByNameAsync(string name, bool includeEvents)
        {
            var query = _context.Lecturer.Include(x => x.SocialNetworks);
            
            if(includeEvents){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Event);
            }
            
            return await query.Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(x => x.Id).ToArrayAsync();
        }

        public async Task<Lecturer> GetLecturerByIdAsync(int id, bool includeEvents)
        {
            var query = _context.Lecturer.Include(x => x.SocialNetworks);
            
            if(includeEvents){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Event);
            }
            
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}