using System.Threading.Tasks;

using ProEventos.Domain;
using ProEventos.Application.ViewModels;
using System.Collections.Generic;
using ProEventos.Repository.Models;

namespace ProEventos.Application.Interfaces
{
    public interface IEventService
    {
         Task<EventViewModel> CreateEventAsync(int userId, EventViewModel model);
         Task<EventViewModel> UpdateEventAsync(int userId, int eventId, EventViewModel model);
         Task<bool> DeleteEventAsync(int userId, int eventId);
         Task<PageList<Event>> GetAllEventsAsync(int userId, PageParams pageParams, bool includeLecturers = false);
         Task<IEnumerable<EventViewModel>> GetAllEventsByNameAsync(int userId, string name, bool includeLecturers = false);
         Task<EventViewModel> GetEventByIdAsync(int userId, int id, bool includeLecturers = false);
    }
}