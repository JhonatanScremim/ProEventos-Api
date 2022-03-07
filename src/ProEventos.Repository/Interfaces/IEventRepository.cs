using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Repository.Models;

namespace ProEventos.Repository.Interfaces
{
    public interface IEventRepository
    {
         IQueryable<Event> GetAllEvents(int userId, bool includeLecturers = false);
         Task<PageList<Event>> GetAllEventsPaginatedAsync(int userId, PageParams pageParams, bool includeLecturers = false);
         Task<IEnumerable<Event>> GetAllEventsByNameAsync(int userId, string name, bool includeLecturers = false);
         Task<Event> GetEventByIdAsync(int userId, int id, bool includeLecturers = false);
    }
}