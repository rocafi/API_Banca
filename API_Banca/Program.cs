using API_Banca;
using API_Banca.Data;
using API_Banca.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<AccountServices>();
builder.Services.AddScoped<TransactionServices>();


// -- builder.Services.AddEntityFrameworkSqlite().AddDbContext<SqlContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ApiBancaContext")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<DataContext>(options => options.UseSqlite(connectionString));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //CREA Y ELIMINA LAS ENTIDADES CADA VEZ QUE SE NECESITAN SIN USAR MIGRACIONES
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
