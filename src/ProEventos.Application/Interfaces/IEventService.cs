using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Interfaces
{
    public interface IEventService
    {
         Task<Event> CreateEventAsync(Event model);
         Task<Event> UpdateEventAsync(int eventId, Event model);
         Task<bool> DeleteEventAsync(int eventId);
         Task<Event[]> GetAllEventsAsync(bool includeLecturers = false);
         Task<Event[]> GetAllEventsByNameAsync(string name, bool includeLecturers = false);
         Task<Event> GetEventByIdAsync(int id, bool includeLecturers = false);
    }
}