using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Repository.Context;
using ProEventos.Domain;
using ProEventos.Repository.Interfaces;

namespace ProEventos.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _context;
        public EventRepository(DataContext context)
        {
            _context = context;
        }
         public async Task<Event[]> GetAllEventsAsync(bool includeLecturers)
        {
            var query = _context.Event.Include(x => x.Batches).Include(x => x.SocialNetworks);
            
            if(includeLecturers){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Lecturer);
            }
            
            return await query.AsNoTracking().OrderBy(x => x.Id).ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsByNameAsync(string name, bool includeLecturers)
        {
            var query = _context.Event.Include(x => x.Batches).Include(x => x.SocialNetworks);
            
            if(includeLecturers){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Lecturer);
            }
            
            return await query.AsNoTracking().Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(x => x.Id).ToArrayAsync();
        }


        public async Task<Event> GetEventByIdAsync(int id, bool includeLecturers)
        {
            var query = _context.Event.Include(x => x.Batches).Include(x => x.SocialNetworks);
            
            if(includeLecturers){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Lecturer);
            }
            
            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}