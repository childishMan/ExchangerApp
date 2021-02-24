using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ExchangerApp.Models;
using Newtonsoft.Json.Linq;

namespace ExchangerApp.Services
{
    class RequestModel
    {
        public Rates rates { get; set; }
        public string _base { get; set; }
        public string date { get; set; }
    }

    class Rates
    {
        public float CHF { get; set; }
        public float EUR { get; set; }
        public float USD { get; set; }
        public float GBP { get; set; }
    }

    public class ExchangeService : IExchangeService
    {
        public async Task<ExchangeModel> ExchangeMoney(ExchangeModel model)
        {
            bool firstMain = true;

            if (model.FirstCurrencyAmount == null && model.SecondCurrencyAmount == null)
                return null;

            try
            {
                string mainCurrency = model.FirstCurrencyCode, secondaryCurrency = model.SecondCurrencyCode;

                if (model.FirstCurrencyAmount == null || model.FirstCurrencyAmount == 0)
                {
                    mainCurrency = model.SecondCurrencyCode;
                    secondaryCurrency = model.FirstCurrencyCode;

                    firstMain = false;
                }

                using var api = new HttpClient();
                var json = JObject.Parse(await api.GetStringAsync(
                    @$"https://api.exchangeratesapi.io/latest?base={mainCurrency}&symbols={secondaryCurrency}"));

                var rate = (float) json["rates"]?[secondaryCurrency];

                model.Rate = rate;

                if (firstMain)
                {
                    model.SecondCurrencyAmount = model.FirstCurrencyAmount * rate;
                }
                else
                {
                    model.FirstCurrencyAmount = model.SecondCurrencyAmount * rate;
                }

                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
