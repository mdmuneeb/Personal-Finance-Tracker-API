using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE.Models;
using SE.Services.Inteface;
using System.Runtime.InteropServices;

namespace SE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoal _goalService;
        public GoalController(IGoal goalService)
        {
            _goalService = goalService;
        }

        [HttpPost]
        [Route("PostGoal")]
        public async Task<IActionResult> PostPostGoal([FromBody] Goal goal)
        {
            var data = await _goalService.AddGoal(goal);
            return Ok(data);

        }

        [HttpGet]
        [Route("GetAllGoalByUserId/{userId}")]
        public async Task<IActionResult> GetAllGoalByUserId(int userId)
        {
            var data = await _goalService.GetGoalByUserId(userId);
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateGoal")]
        public async Task<IActionResult> UpdateGoal([FromBody] Goal goal)
        {
            var data = await _goalService.UpdateGoalByGoalId(goal);
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteGoalPermanently/{GId}/{UserId}")]
        public async Task<IActionResult> DeleteGoalPermanent(int UserId, int GId)   
        {
            var data = await _goalService.deleteGoal(UserId, GId);
            return Ok(data);
        }

    }
}
