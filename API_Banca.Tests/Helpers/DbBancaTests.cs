using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Banca.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public static class DbBancaTests
{
    public static DataContext CreateInMemoryDb()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(connection)
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();

        return context;
    }
}