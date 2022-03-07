using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Repository.Context;
using ProEventos.Domain;
using ProEventos.Repository.Interfaces;
using System.Collections.Generic;
using ProEventos.Repository.Models;

namespace ProEventos.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _context;
        public EventRepository(DataContext context)
        {
            _context = context;
        }
         public IQueryable<Event> GetAllEvents(int userId, bool includeLecturers)
        {
            var query = _context.Event.Include(x => x.Batches).Include(x => x.SocialNetworks);
            
            if(includeLecturers){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Lecturer);
            }
            
            return query.AsNoTracking().Where(x => x.UserId == userId).OrderBy(x => x.Id);
        }

        public async Task<PageList<Event>> GetAllEventsPaginatedAsync(int userId, PageParams pageParams, bool includeLecturers)
        {
            var query = _context.Event.Include(x => x.Batches).Include(x => x.SocialNetworks);
            
            if(includeLecturers)
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Lecturer);
            
            var response = query.AsNoTracking().Where(x => x.UserId == userId).OrderBy(x => x.Id);
            
            return await PageList<Event>.CreateAsync(response, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<IEnumerable<Event>> GetAllEventsByNameAsync(int userId, string name, bool includeLecturers)
        {
            var query = _context.Event.Include(x => x.Batches).Include(x => x.SocialNetworks);
            
            if(includeLecturers){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Lecturer);
            }
            
            return await query.AsNoTracking().Where(x => x.Name.ToLower().Contains(name.ToLower()) && x.UserId == userId)
                .OrderBy(x => x.Id).ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int userId, int id, bool includeLecturers)
        {
            var query = _context.Event.Include(x => x.Batches).Include(x => x.SocialNetworks);
            
            if(includeLecturers){
                query.Include(x => x.EventLecturers).ThenInclude(x => x.Lecturer);
            }
            
            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }
    }
}