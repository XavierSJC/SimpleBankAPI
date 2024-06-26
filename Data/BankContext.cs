using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Data
{
    public class BankContext: DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }
        
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}
