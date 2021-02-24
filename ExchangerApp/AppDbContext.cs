using ExchangerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangerApp
{
    public class AppDbContext:DbContext
    {
        public DbSet<HistoryModel> historyModels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
    }
}
