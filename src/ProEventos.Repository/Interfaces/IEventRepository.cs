using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Repository.Interfaces
{
    public interface IEventRepository
    {
         Task<Event[]> GetAllEventsAsync(bool includeLecturers = false);
         Task<Event[]> GetAllEventsByNameAsync(string name, bool includeLecturers = false);
         Task<Event> GetEventByIdAsync(int id, bool includeLecturers = false);
    }
}