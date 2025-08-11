using Microsoft.AspNetCore.Mvc;
using API_Banca.Models;
using API_Banca.Services;

namespace API_Banca.Controllers
{
    [ApiController]
    [Route("API/Banca/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountServices _accountService;

        public AccountController(AccountServices accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("Search/{number}")]
        public async Task<IEnumerable<Account?>> GetByNumber(string number)
        {
            return await _accountService.GetAccountByNumberAsync(number);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDTO dto)
        {
            try
            {
                var account = await _accountService.CreateAccountAsync(dto);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}