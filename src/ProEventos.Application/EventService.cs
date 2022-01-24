using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Interfaces;
using ProEventos.Application.ViewModels;
using ProEventos.Domain;
using ProEventos.Repository.Interfaces;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IBaseRepository baseRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventViewModel> CreateEventAsync(EventViewModel model)
        {
            _baseRepository.Create<Event>(_mapper.Map<Event>(model));

            if(await _baseRepository.SaveChangesAsync()) 
                return model;

            return null;
        }
        
        public async Task<EventViewModel> UpdateEventAsync(int eventId, EventViewModel model)
        {
            if(eventId == default)
                throw new Exception("Event ID parameter is required!"); 

            var oldEvent = await _eventRepository.GetEventByIdAsync(eventId, false);
            if(oldEvent == null) 
                return null;

            if (model.Id == default)
                model.Id = oldEvent.Id;

            _baseRepository.Update<Event>(_mapper.Map<Event>(model));

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

        public async Task<EventViewModel[]> GetAllEventsAsync(bool includeLecturers = false)
        {
            var events = await _eventRepository.GetAllEventsAsync(includeLecturers);
            
            if(events == null)
                return null;
                
            return _mapper.Map<EventViewModel[]>(events);
        }

        public async Task<EventViewModel[]> GetAllEventsByNameAsync(string name, bool includeLecturers = false)
        {
            var events = await _eventRepository.GetAllEventsByNameAsync(name, includeLecturers);

            if(events == null)
                return null;

            return _mapper.Map<EventViewModel[]>(events);
        }

        public async Task<EventViewModel> GetEventByIdAsync(int id, bool includeLecturers = false)
        {
            var response =  await _eventRepository.GetEventByIdAsync(id, includeLecturers);

            if(response == null)
                return null;

            return _mapper.Map<EventViewModel>(response);
        }
    }
}