using API_Banca.Data;
using API_Banca.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Banca.Services
{
    public class UserServices
    {
        private readonly DataContext _context;

        public UserServices(DataContext context)
        {
            _context = context;
        }

        // CREAR USUARIO
        public async Task<User> CreateUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // OBTENER USUARIO POR NOMBRE
        public async Task<List<User?>> GetUserByNameAsync(string name)
        {
            var result = await (from user in _context.User
                                join account in _context.Account
                                on user.UserID equals account.ClientID into userAccounts
                                where user.Name == name
                                from ua in userAccounts.DefaultIfEmpty()
                                select new User
                                {
                                    UserID = user.UserID,
                                    Name = user.Name,
                                    Birthday = user.Birthday,
                                    Gender = user.Gender,
                                    Incommes = user.Incommes,
                                    CreatedAt = user.CreatedAt,
                                    AccountNumber = ua != null ? ua.AccountNumber : null
                                }).ToListAsync();

            return result;
        }

    }
}
