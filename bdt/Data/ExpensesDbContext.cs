using bdt.Models;
using Microsoft.EntityFrameworkCore;

namespace bdt.Data
{
    public class ExpensesDbContext : DbContext
    {
        public DbSet<Expense> expenses { get; set; }
        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
        {

        }
    }
}
