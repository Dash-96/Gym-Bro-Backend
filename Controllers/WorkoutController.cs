

using GymBroAspBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        private WorkoutService _service;
        public WorkoutController(WorkoutService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workouts = await _service.GetWorkoutsAsync();
            return Ok(workouts);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> Create([FromBody] WorkoutDto dto)
        {
            await _service.CreateWorkout(dto);
            return CreatedAtAction(nameof(Create) ,dto);
            // return await CreatedAtAction(response);
        }
    }
}
