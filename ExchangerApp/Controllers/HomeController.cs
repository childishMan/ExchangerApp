using System;
using ExchangerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using ExchangerApp.Facades;
using ExchangerApp.Services;
using Microsoft.AspNetCore.Http;

namespace ExchangerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbService _dbService;
        private readonly IExchangeFacade _exchangeFacade;

        public HomeController(ILogger<HomeController> logger, IExchangeFacade exchangeFacade, IDbService dbService)
        {
            _logger = logger;
            _exchangeFacade = exchangeFacade ?? throw new ArgumentNullException(nameof(exchangeFacade));
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

            try
            {
                var res = await _exchangeFacade.ExchangeMoney(model);

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
            catch (ArgumentNullException x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
