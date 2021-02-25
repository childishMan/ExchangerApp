using System.Threading.Tasks;
using ExchangerApp.Models;

namespace ExchangerApp.Services
{
    public interface IExchangeService
    {
        Task<float> GetRate(string mainCurrency, string secondaryCurrency);
    }
}
