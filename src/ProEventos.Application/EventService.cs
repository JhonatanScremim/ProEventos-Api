using System.Linq;
using System.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Interfaces;
using ProEventos.Application.ViewModels;
using ProEventos.Domain;
using ProEventos.Repository.Interfaces;
using ProEventos.Repository.Models;

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

        public async Task<EventViewModel> CreateEventAsync(int userId, EventViewModel model)
        {
            var newEvent = _mapper.Map<Event>(model);
            newEvent.UserId = userId;

            _baseRepository.Create<Event>(newEvent);

            if(await _baseRepository.SaveChangesAsync()) 
                return model;

            return null;
        }
        
        public async Task<EventViewModel> UpdateEventAsync(int userId, int eventId, EventViewModel model)
        {
            if(eventId == default)
                throw new Exception("Event ID parameter is required!"); 

            var oldEvent = await _eventRepository.GetEventByIdAsync(userId, eventId, false);
            if(oldEvent == null) 
                return null;

            if (model.Id == default)
                model.Id = oldEvent.Id;

            _baseRepository.Update<Event>(_mapper.Map<Event>(model));

            if(await _baseRepository.SaveChangesAsync()) 
                return model;

            return null;
        }

        public async Task<bool> DeleteEventAsync(int userId, int eventId)
        {
            if(eventId == default)
                throw new Exception("Event ID is required!");

            var oldEvent = await _eventRepository.GetEventByIdAsync(userId, eventId, false);
            if(oldEvent == null) 
                throw new Exception("Event not find");
            
            _baseRepository.Delete<Event>(oldEvent);

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<PageList<Event>> GetAllEventsAsync(int userId, PageParams pageParams, bool includeLecturers = false)
        {
            var events = _eventRepository.GetAllEvents(userId, includeLecturers);
            var page = await PageList<Event>.CreateAsync(events, pageParams.PageNumber, pageParams.PageSize);

            return page;
        }

        public async Task<IEnumerable<EventViewModel>> GetAllEventsByNameAsync(int userId, string name, bool includeLecturers = false)
        {
            var events = await _eventRepository.GetAllEventsByNameAsync(userId, name, includeLecturers);
            if(events == null)
                return null;
            return _mapper.Map<IEnumerable<EventViewModel>>(events);
        }

        public async Task<EventViewModel> GetEventByIdAsync(int userId, int id, bool includeLecturers = false)
        {
            var response =  await _eventRepository.GetEventByIdAsync(userId, id, includeLecturers);

            if(response == null)
                return null;

            return _mapper.Map<EventViewModel>(response);
        }
    }
}