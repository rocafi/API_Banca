using API_Banca.Models;
using API_Banca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace API_Banca.Controllers
{
    [ApiController]
    [Route("API/Banca/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionServices _transactionService;
        public TransactionController(TransactionServices transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO transactionDto)
        {
            try
            {
                var transaction = await _transactionService.CreateTransactionAsync(transactionDto);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("Search/{number}")]
        public async Task<IEnumerable<Transaction?>> GetByNumber(string number)
        {
            return await _transactionService.GetAllTransactionsByNumberAsync(number);
        }
    }
    
}
