using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ExchangerApp.Models;
using Newtonsoft.Json.Linq;

namespace ExchangerApp.Services
{
    public class ExchangeService : IExchangeService
    {
        public async Task<float> GetRate(string mainCurrency,string secondaryCurrency)
        {
            try
            {
                using var api = new HttpClient();
                var json = JObject.Parse(await api.GetStringAsync(
                    @$"https://api.exchangeratesapi.io/latest?base={mainCurrency}&symbols={secondaryCurrency}"));

                return (float) json["rates"]?[secondaryCurrency];
            }
            catch
            {
                throw new FileLoadException();
            }
        }
    }
}
