using System.Threading.Tasks;
using ExchangerApp.Models;

namespace ExchangerApp.Services
{
    public interface IExchangeService
    {
        Task<ExchangeModel> ExchangeMoney(ExchangeModel model);
    }
}
