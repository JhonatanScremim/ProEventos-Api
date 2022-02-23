using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Repository.Interfaces
{
    public interface IEventRepository
    {
         Task<IEnumerable<Event>> GetAllEventsAsync(int userId, bool includeLecturers = false);
         Task<IEnumerable<Event>> GetAllEventsByNameAsync(int userId, string name, bool includeLecturers = false);
         Task<Event> GetEventByIdAsync(int userId, int id, bool includeLecturers = false);
    }
}