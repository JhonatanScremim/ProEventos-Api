using System.Threading.Tasks;

using ProEventos.Domain;
using ProEventos.Application.ViewModels;

namespace ProEventos.Application.Interfaces
{
    public interface IEventService
    {
         Task<EventViewModel> CreateEventAsync(EventViewModel model);
         Task<EventViewModel> UpdateEventAsync(int eventId, EventViewModel model);
         Task<bool> DeleteEventAsync(int eventId);
         Task<EventViewModel[]> GetAllEventsAsync(bool includeLecturers = false);
         Task<EventViewModel[]> GetAllEventsByNameAsync(string name, bool includeLecturers = false);
         Task<EventViewModel> GetEventByIdAsync(int id, bool includeLecturers = false);
    }
}