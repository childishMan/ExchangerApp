using System.Collections.Generic;
using ExchangerApp.Models;

namespace ExchangerApp.Services
{
    public interface IDbService
    {
        IEnumerable<HistoryModel> GetAllModels();

        void AddEntry(HistoryModel model);
    }
}
