using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Application.ViewModels;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet("GetByEvent")]
        public async Task<IActionResult> GetBatchesByEventIdAsync([FromQuery] int eventId){
            try{
                var response = await _batchService.GetBatchesByEventIdAsync(eventId);

                if(response == null)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpGet("GetByEventAndById")]
        public async Task<IActionResult> GetBatchByIdsAsync([FromQuery] int eventId, int batchId){
            try{
                var response = await _batchService.GetBatchByIdsAsync(eventId, batchId);

                if(response == null)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception e){
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> SaveBathesAsync([FromQuery] int eventId,
            [FromBody] List<BatchViewModel> models){
                try{
                    var response = await _batchService.SaveBathesAsync(eventId, models);

                    if(response == null)
                    return NoContent();

                    return Ok(response);
                }
                catch(Exception e){
                    return this.StatusCode(StatusCodes.Status500InternalServerError,
                    "Error: " + e.Message);
                }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int eventId, int batchId){
            try{
                return await _batchService.DeleteAsync(eventId, batchId) ? 
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