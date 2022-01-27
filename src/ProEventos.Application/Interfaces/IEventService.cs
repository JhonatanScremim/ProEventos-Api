using System.Threading.Tasks;

using ProEventos.Domain;
using ProEventos.Application.ViewModels;
using System.Collections.Generic;

namespace ProEventos.Application.Interfaces
{
    public interface IEventService
    {
         Task<EventViewModel> CreateEventAsync(EventViewModel model);
         Task<EventViewModel> UpdateEventAsync(int eventId, EventViewModel model);
         Task<bool> DeleteEventAsync(int eventId);
         Task<IEnumerable<EventViewModel>> GetAllEventsAsync(bool includeLecturers = false);
         Task<IEnumerable<EventViewModel>> GetAllEventsByNameAsync(string name, bool includeLecturers = false);
         Task<EventViewModel> GetEventByIdAsync(int id, bool includeLecturers = false);
    }
}