using System.Collections.Generic;
using System.Linq;
using ExchangerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangerApp.Services
{
    public class DbService:IDbService
    {
        private readonly AppDbContext _dbContext;

        public DbService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<HistoryModel> GetAllModels()
        {
            return _dbContext.historyModels.AsNoTracking().OrderByDescending(e=>e.Date);
        }

        public void AddEntry(HistoryModel model)
        {
            if (model != null)
            {
                _dbContext.historyModels.Add(model);
                _dbContext.SaveChanges();
            }
        }
    }
}
