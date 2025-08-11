using API_Banca.Data;
using API_Banca.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace API_Banca.Services
{
    public class AccountServices
    {
        private readonly DataContext _context;
        public AccountServices(DataContext context)
        {
            _context = context;
        }

        // OBTENER DATOS DE CUENTA POR NUMERO DE CUENTA
        public async Task<List<Account?>> GetAccountByNumberAsync(string number)
        {
            var result = await (from account in _context.Account
                                where account.AccountNumber == number
                                select new Account
                                {
                                    AccountID = account.AccountID,
                                    AccountNumber = account.AccountNumber,
                                    Balance = account.Balance,
                                    ClientID = account.ClientID
                                }).ToListAsync();

            return result;
        }

        // CREAR CUENTA (unica cuenta por usuario)
        public async Task<Account> CreateAccountAsync(AccountDTO accountDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Name == accountDto.UserName);
            var accountExists = await _context.Account.AnyAsync(a => a.AccountNumber == user.AccountNumber);

            // Validaciones
            if (user == null)
                throw new Exception("El usuario no existe.");
            if (accountExists)
                throw new Exception("El usuario ya tiene una cuenta.");

            if (accountDto.Balance <= 0)
                throw new Exception("El monto para crear una cuenta debe ser mayor que 0");
            if (accountDto.UserName == null)
                throw new Exception("El nombre de la cuenta no puede estar vacío.");


            var accountNumber = $"25{user.UserID:D4}";
            var account = new Account
            {
                ClientID = user.UserID,
                AccountNumber = accountNumber,
                Balance = accountDto.Balance
            };

            _context.Account.Add(account);

            user.AccountNumber = accountNumber;

            await _context.SaveChangesAsync();

            return account;
        }

    }
}
