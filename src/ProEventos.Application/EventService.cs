using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Repository.Interfaces;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IEventRepository _eventRepository;

        public EventService(IBaseRepository baseRepository, IEventRepository eventRepository)
        {
            _baseRepository = baseRepository;
            _eventRepository = eventRepository;
        }

        public async Task<Event> CreateEventAsync(Event model)
        {
            _baseRepository.Create<Event>(model);

            if(await _baseRepository.SaveChangesAsync()) 
                return model;

            return null;
        }
        
        public async Task<Event> UpdateEventAsync(int eventId, Event model)
        {
            if(model.Id == default)
                throw new Exception("Event ID is required!"); 

            var oldEvent = await _eventRepository.GetEventByIdAsync(eventId, false);
            if(oldEvent == null) 
                return null;

            _baseRepository.Update<Event>(model);

            if(await _baseRepository.SaveChangesAsync()) 
                return model;

            return null;
        }

        public async Task<bool> DeleteEventAsync(int eventId)
        {
            if(eventId == default)
                throw new Exception("Event ID is required!");

            var oldEvent = await _eventRepository.GetEventByIdAsync(eventId, false);
            if(oldEvent == null) 
                throw new Exception("Event not find");
            
            _baseRepository.Delete<Event>(oldEvent);

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeLecturers = false)
        {
            return await _eventRepository.GetAllEventsAsync(includeLecturers);
        }

        public async Task<Event[]> GetAllEventsByNameAsync(string name, bool includeLecturers = false)
        {
            return await _eventRepository.GetAllEventsByNameAsync(name, includeLecturers);
        }

        public async Task<Event> GetEventByIdAsync(int id, bool includeLecturers = false)
        {
            return await _eventRepository.GetEventByIdAsync(id, includeLecturers);
        }
    }
}