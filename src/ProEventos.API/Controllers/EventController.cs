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
        public async Task<IActionResult> GetEventById([FromQuery] int id){
            try{

                var response = await _eventService.GetEventByIdAsync(id, true);

                if(response == null)
                    return NotFound("Event not found");

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetEventByName([FromQuery] string name){
            try{
                var response = await _eventService.GetAllEventsByNameAsync(name, true);

                if(response == null)
                    return NotFound("Events not found");

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event model){
            try{
                var response = await _eventService.CreateEventAsync(model);

                if(response == null)
                    return BadRequest("Unable to register");

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromQuery] int id, [FromBody] Event model){
            try{
                var response = await _eventService.UpdateEventAsync(id, model);

                if(response == null)
                    return BadRequest("Unable to update");

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromQuery] int id){
            try{
                return await _eventService.DeleteEventAsync(id) ? BadRequest("Unable to delete") : Ok("Success");
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

    }
}
