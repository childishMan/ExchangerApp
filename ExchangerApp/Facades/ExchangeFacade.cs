using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangerApp.Models;
using ExchangerApp.Services;

namespace ExchangerApp.Facades
{
    public class ExchangeFacade:IExchangeFacade
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeFacade(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService ?? throw new ArgumentNullException(nameof(exchangeService));
        }

        public async Task<ExchangeModel> ExchangeMoney(ExchangeModel model)
        {
            if ((model.FirstCurrencyAmount == null || model.FirstCurrencyAmount <= 0) &&
                (model.SecondCurrencyAmount == null || model.SecondCurrencyAmount <= 0))
            {
                throw new ArgumentNullException(nameof(model),"You can't leave both input fields empty");
            }

            bool firstMain = true;

            var mainCurrency = model.FirstCurrencyCode;
            var secondaryCurrency = model.SecondCurrencyCode;

            if (model.FirstCurrencyAmount == null || model.FirstCurrencyAmount == 0)
            {
                mainCurrency = model.SecondCurrencyCode;
                secondaryCurrency = model.FirstCurrencyCode;

                firstMain = false;
            }

            var rate = await _exchangeService.GetRate(mainCurrency, secondaryCurrency);

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
    }
}
