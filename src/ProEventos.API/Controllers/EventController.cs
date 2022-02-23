using System.IO;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using ProEventos.API.Extensions;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUserService _userService;

        public EventController(IEventService eventService, IWebHostEnvironment hostEnvironment, IUserService userService)
        {
            _eventService = eventService;
            _hostEnvironment = hostEnvironment;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            try{
                return Ok(await _eventService.GetAllEventsAsync(User.GetUserId(), true));
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
        
        [HttpGet("GetById")]
        public async Task<IActionResult> GetEventById([FromQuery] int id){
            try{

                var response = await _eventService.GetEventByIdAsync(User.GetUserId(), id, true);

                if(response == null)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetEventByName([FromQuery] string name){
            try{
                var response = await _eventService.GetAllEventsByNameAsync(User.GetUserId(), name, true);

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
                var response = await _eventService.CreateEventAsync(User.GetUserId(), model);

                if(response == null)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPost("UploadImage/{eventId}")]
        public async Task<IActionResult> UploadImage(int eventId){
            try{
                
                var oldEvent = await _eventService.GetEventByIdAsync(User.GetUserId(), eventId);
                if(oldEvent == null)
                    return NoContent();

                var file = Request.Form.Files[0];

                if(file.Length > 0)
                {
                    DeleteImage(oldEvent.ImageUrl);
                    oldEvent.ImageUrl = await SaveImage(file);
                }
                var response = await _eventService.UpdateEventAsync(User.GetUserId(), eventId, oldEvent);
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
                var response = await _eventService.UpdateEventAsync(User.GetUserId(), id, model);

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
                return await _eventService.DeleteEventAsync(User.GetUserId(), id) ? 
                    Ok("Success") : 
                    throw new Exception("Unable to delete, review your parameters");
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile){
            
            var imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                            .Take(10).ToArray()).Replace(" ", "-");

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.Name)}";                            

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/ImageS", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName){
            
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
            if(System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

    }
}
