using Microsoft.EntityFrameworkCore;
using API_Banca.Models;

namespace API_Banca.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Name)
                      .IsUnique();
                entity.Property(u => u.CreatedAt)
              .HasDefaultValueSql("CURRENT_DATE");
            });
            //modelBuilder.Entity<Account>(entity =>
            //{
            //    entity.HasIndex(a => a.AccountNumber)
            //          .IsUnique();
            //});
        }
    }
}
