using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegAjax.Data.Entities;
using RegAjax.Models;
using RegAjax.Services;

namespace RegAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnswerService _answerService;
        private readonly IRegistrationService _registrationService;
        private readonly IVariantService _variantService;

        public HomeController(ILogger<HomeController> logger, IAnswerService answerService,
            IRegistrationService registrationService, IVariantService variantService)
        {
            _logger = logger;

            _answerService = answerService;
            _registrationService = registrationService;
            _variantService = variantService;
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

        [HttpPost]
        public async Task<IActionResult> CreateRegistration(RegistrationModel registrationModel, CancellationToken cancel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var registry = new Registration();
            registry.FirstName = registrationModel.FirstName;
            registry.SecondName = registrationModel.SecondName;
            registry.Phone = registrationModel.Phone;

            await _registrationService.Save(registry, cancel);
            
            foreach (var questionModel in registrationModel.Questions)
            {
                foreach (var variantModel in questionModel.Variants)
                {
                    if (variantModel.Checked)
                    { 
                        var answer = new Answer();
                        answer.RegistrationId = registry.Id;
                        answer.VariantId = variantModel.Id;

                        await _answerService.Save(answer, cancel);
                    }
                }
            }

            return Ok("Success");
        }
    }
}