using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using SE.Models;
using SE.Models.DTOS;
using SE.Services.Inteface;
using SE.Services.Services;

namespace SE_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transactionService;
        public TransactionController(ITransaction transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpPost]
        [Route("PostTransaction")]
        public async Task<IActionResult> PostTransaction(TransactionDTO transactionDTO)
        {
            var data = await _transactionService.AddTransaction(transactionDTO);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetTransactionById/{userId}")]
        public async Task<IActionResult> GettransById(int userId) 
        {

            var data = await _transactionService.GetAllTransaction(userId);
            return Ok(data);
            
        }

        [HttpPut]
        [Route("UpdateTransaction")]
        public async Task<IActionResult> PutTransaction([FromBody] Transaction transactionData)
        {
            var data = await _transactionService.UpdateTransaction(transactionData);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetTransactionByUserIdTransactionId/{userId}/{transactionId}")]
        public async Task<IActionResult> getTransactionByTransactionIdUserId(int userId, int transactionId)
        {
            var data = await _transactionService.GetTransactionById(userId, transactionId);
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteTransactionPermanent/{UserId}/{TId}")]
        public async Task<IActionResult> DeleteTransaction(int TId, int UserId)
        {
            var data = await _transactionService.DeleteTransaction(TId, UserId);
            return Ok(data);
        } 

    }
}
