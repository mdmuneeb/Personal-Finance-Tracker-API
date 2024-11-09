using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE.Models;
using SE.Services.Inteface;
using System.Runtime.InteropServices;

namespace SE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepeatedTransactionController : ControllerBase
    {
        private IRepeatedTransaction _repeatedTransaction;
        public RepeatedTransactionController(IRepeatedTransaction repeatedTransaction)
        {
            _repeatedTransaction = repeatedTransaction;
        }

        [HttpGet]
        [Route("GetAllTransaction/{userID}")]
        public async Task<IActionResult> GetAllTransaction(int userID)
        {
            var data = await _repeatedTransaction.GetAllRepeatedTransactionList(userID);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetTranaction/{userID}/{transactionData}")]
        public async Task<IActionResult> GetTranaction(int userID, int transactionData)
        {
            var data = await _repeatedTransaction.GetASingleTransaction(userID, transactionData);
            return Ok(data);
        }

        [HttpPost]
        [Route("PostRepeatedTransaction")]
        public async Task<IActionResult> PostRepeatedTransaction(RepeatedTransaction rdata)
        {
            var data = await _repeatedTransaction.AddGoal(rdata);
            return Ok(data);
        }

        [HttpPut]
        [Route("EditTransaction")]
        public async Task<IActionResult> UpdateTransaction(RepeatedTransaction rdata)
        {
            var data = await _repeatedTransaction.UpdateATransaction(rdata);
            return Ok(data);
        }
    }
}
