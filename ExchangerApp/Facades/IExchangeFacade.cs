using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangerApp.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ExchangerApp.Facades
{
    public interface IExchangeFacade
    {
        Task<ExchangeModel> ExchangeMoney(ExchangeModel model);
    }
}
