using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegAjax.Models;
using RegAjax.Services;

namespace RegAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegistrationService _registrationService;

        public HomeController(ILogger<HomeController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        public async Task<IActionResult> Index(CancellationToken cancel)
        {
            var questions = await _registrationService.GetAsync(cancel);
            
            var model = new RegistrationModel()
            {
                FirstName = "",
                SecondName = "",
                Phone = "",
                Questions = questions.Select(q => new QuestionModel()
                {
                    Id = q.Id,
                    Name = q.Name,
                    Variants = q.Variants.Select(v => new VariantModel()
                    {
                        Id = v.Id,
                        Name = v.Name
                    }).ToList()
                }).ToList()
            };
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}