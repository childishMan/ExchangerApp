using System;
using ExchangerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using ExchangerApp.Services;

namespace ExchangerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExchangeService _exchangeService;
        private readonly IDbService _dbService;

        public HomeController(ILogger<HomeController> logger,IExchangeService exchangeService,IDbService dbService)
        {
            _logger = logger;
            _exchangeService = exchangeService ?? throw new ArgumentNullException(nameof(exchangeService));
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            var model = _dbService.GetAllModels();
            ViewBag.Items = model;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExchangeEndpoint(ExchangeModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var res = await _exchangeService.ExchangeMoney(model);

            if (res != null)
            {
                var md = new HistoryModel()
                {
                    Date = DateTime.Now,
                    FirstCurrencyAmount = res.FirstCurrencyAmount,
                    FirstCurrencyCode = res.FirstCurrencyCode,
                    SecondCurrencyAmount = res.SecondCurrencyAmount,
                    SecondCurrencyCode = res.SecondCurrencyCode
                };

                _dbService.AddEntry(md);
            }

            return Ok(res);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
