using API_Banca.Models;
using API_Banca.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


public class BancaTests
{
    [Fact]
    public async Task AddUserTest()
    {
        var context = DbBancaTests.CreateInMemoryDb();
        var userService = new UserServices(context);
        var accountService = new AccountServices(context);

        // Crear usuario
        var user = new User
        {
            Name = "Carlos",
            Birthday = new DateOnly(1990, 1, 1),
            Gender = 'M',
            Incommes = 2000m,
            CreatedAt = DateOnly.FromDateTime(System.DateTime.Now)
        };
        var createdUser = await userService.CreateUserAsync(user);

        // Crear cuenta
        var accountDto = new AccountDTO
        {
            UserName = createdUser.Name,
            Balance = 1000m
        };
        var createdAccount = await accountService.CreateAccountAsync(accountDto);

        Assert.NotNull(createdAccount);
        Assert.Equal(createdUser.UserID, createdAccount.ClientID);
        Assert.Equal(1000m, createdAccount.Balance);
        Assert.StartsWith("25", createdAccount.AccountNumber);
    }

    [Fact]
    public async Task DepositTest()
    {
        var context = DbBancaTests.CreateInMemoryDb();

        var userService = new UserServices(context);
        var accountService = new AccountServices(context);
        var transactionService = new TransactionServices(context);

        // Crear usuario y cuenta
        var user = new User { Name = "Ana", Birthday = new DateOnly(1995, 5, 5), Gender = 'F', Incommes = 1500m, CreatedAt = DateOnly.FromDateTime(System.DateTime.Now) };
        var createdUser = await userService.CreateUserAsync(user);
        var account = await accountService.CreateAccountAsync(new AccountDTO { UserName = createdUser.Name, Balance = 500m });

        var depositDto = new TransactionDTO
        {
            AccountNumber = account.AccountNumber,
            TransactionType = "D",
            Amount = 300m
        };

        var transaction = await transactionService.CreateTransactionAsync(depositDto);

        // Verificar saldo actualizado
        var updatedAccount = await context.Account.FindAsync(account.AccountID);

        Assert.NotNull(transaction);
        Assert.Equal("D", transaction.TransactionType);
        Assert.Equal(300m, transaction.Amount);
        //Assert.Equal(500m, transaction.InitialBalance);
        Assert.Equal(800m, transaction.FinalBalance);
        Assert.Equal(800m, updatedAccount.Balance);
    }

    [Fact]
    public async Task WithdrawalTest()
    {
        var context = DbBancaTests.CreateInMemoryDb();

        var userService = new UserServices(context);
        var accountService = new AccountServices(context);
        var transactionService = new TransactionServices(context);

        // Crear usuario y cuenta
        var user = new User { Name = "Luis", Birthday = new DateOnly(1985, 3, 10), Gender = 'M', Incommes = 2500m, CreatedAt = DateOnly.FromDateTime(System.DateTime.Now) };
        var createdUser = await userService.CreateUserAsync(user);
        var account = await accountService.CreateAccountAsync(new AccountDTO { UserName = createdUser.Name, Balance = 1000m });

        // Crear retiro
        var retiroDto = new TransactionDTO
        {
            AccountNumber = account.AccountNumber,
            TransactionType = "R",
            Amount = 400m
        };

        var transaction = await transactionService.CreateTransactionAsync(retiroDto);

        var updatedAccount = await context.Account.FindAsync(account.AccountID);

        Assert.NotNull(transaction);
        Assert.Equal("R", transaction.TransactionType);
        Assert.Equal(400m, transaction.Amount);
        //Assert.Equal(1000m, transaction.InitialBalance);
        Assert.Equal(600m, transaction.FinalBalance);
        Assert.Equal(600m, updatedAccount.Balance);
    }

    [Fact]
    public async Task ConsultarHistorial_DeberiaRetornarTransacciones()
    {
        var context = DbBancaTests.CreateInMemoryDb();

        var userService = new UserServices(context);
        var accountService = new AccountServices(context);
        var transactionService = new TransactionServices(context);

        // Crear usuario y cuenta
        var user = new User { Name = "Carlos", Birthday = new DateOnly(1980, 6, 15), Gender = 'M', Incommes = 4000m, CreatedAt = DateOnly.FromDateTime(System.DateTime.Now) };
        var createdUser = await userService.CreateUserAsync(user);
        var account = await accountService.CreateAccountAsync(new AccountDTO { UserName = createdUser.Name, Balance = 1500m });

        // Hacer dos transacciones
        await transactionService.CreateTransactionAsync(new TransactionDTO { AccountNumber = account.AccountNumber, TransactionType = "D", Amount = 500m });
        await transactionService.CreateTransactionAsync(new TransactionDTO { AccountNumber = account.AccountNumber, TransactionType = "R", Amount = 300m });

        // Consultar historial
        var transactions = await transactionService.GetAllTransactionsByNumberAsync(account.AccountNumber);

        Assert.NotEmpty(transactions);
        Assert.Equal(2, transactions.Count);
    }

}

