using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE.Models.DTOS;
using SE.Services.Inteface;
using SE.Services.Services;

namespace SE_API.Controllers
{
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
    }
}
