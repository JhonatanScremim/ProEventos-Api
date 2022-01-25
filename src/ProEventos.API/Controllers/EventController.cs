using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Repository;
using ProEventos.Domain;
using ProEventos.Repository.Context;
using ProEventos.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.ViewModels;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            try{
                return Ok(await _eventService.GetAllEventsAsync(true));
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id){
            try{

                var response = await _eventService.GetEventByIdAsync(id, true);

                if(response == null)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetEventByName(string name){
            try{
                var response = await _eventService.GetAllEventsByNameAsync(name, true);

                if(response == null || !response.Any())
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventViewModel model){
            try{
                var response = await _eventService.CreateEventAsync(model);

                if(response == null)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventViewModel model){
            try{
                var response = await _eventService.UpdateEventAsync(id, model);

                if(response == null)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id){
            try{
                return await _eventService.DeleteEventAsync(id) ? 
                    Ok("Success") : 
                    throw new Exception("Unable to delete, review your parameters");
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

    }
}
