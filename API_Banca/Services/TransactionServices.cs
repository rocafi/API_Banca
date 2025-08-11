using API_Banca.Data;
using API_Banca.Models;
using API_Banca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Banca.Services
{
    public class TransactionServices
    {
        private readonly DataContext _context;

        public TransactionServices(DataContext context)
        {
            _context = context;
        }

        // CREAR UNA NUEVA TRANSACCIÓN
        public async Task<Transaction> CreateTransactionAsync(TransactionDTO transactionDto)
        {
            {
                var account = await _context.Account
                    .FirstOrDefaultAsync(a => a.AccountNumber == transactionDto.AccountNumber);

                if (account == null)
                    throw new Exception("La cuenta no existe.");

                if (transactionDto.TransactionType != "D" && transactionDto.TransactionType != "R")
                    throw new Exception("El tipo de transacción debe ser 'D' (depósito) o 'R' (retiro).");

                decimal initialBalance = account.Balance;
                decimal nuevoBalance = account.Balance;

                if (transactionDto.TransactionType == "D")
                {
                    if (transactionDto.Amount <= 0)
                        throw new Exception("El monto del depósito debe ser mayor que cero.");
                    nuevoBalance += transactionDto.Amount;
                }
                else if (transactionDto.TransactionType == "R")
                {
                    if (account.Balance < transactionDto.Amount)
                        throw new Exception("No tienes suficientes fondos para realizar esta acción.");
                    if (transactionDto.Amount <= 0)
                        throw new Exception("El monto del retiro debe ser mayor que cero.");
                    nuevoBalance -= transactionDto.Amount;
                }

                account.Balance = nuevoBalance;

                var transaccion = new Transaction
                {
                    AccountNumber = transactionDto.AccountNumber,
                    TransactionType = transactionDto.TransactionType,
                    Amount = transactionDto.Amount,
                    InitialBalance = initialBalance,
                    FinalBalance = nuevoBalance,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now)
                };

                _context.Transaction.Add(transaccion);

                await _context.SaveChangesAsync();

                return transaccion;
            }

        }

        // OBTENER TODAS LAS TRANSACCIONES POR NÚMERO DE CUENTA
        public async Task<List<Transaction?>> GetAllTransactionsByNumberAsync(string number)
        {
            var result = await (from transaction in _context.Transaction
                                where transaction.AccountNumber == number
                                select new Transaction
                                {
                                    TransactionID = transaction.TransactionID,
                                    AccountNumber = transaction.AccountNumber,
                                    TransactionType = transaction.TransactionType,
                                    Amount = transaction.Amount,
                                    InitialBalance = transaction.InitialBalance,
                                    FinalBalance = transaction.FinalBalance,
                                    CreatedAt = transaction.CreatedAt
                                }).ToListAsync();

            return result;
        }
    }
}
