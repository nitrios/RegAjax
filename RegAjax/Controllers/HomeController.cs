using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IQuestionService _questionService;

        public HomeController(ILogger<HomeController> logger, IAnswerService answerService,
            IRegistrationService registrationService, IVariantService variantService, IQuestionService questionService)
        {
            _logger = logger;

            _answerService = answerService;
            _registrationService = registrationService;
            _variantService = variantService;
            _questionService = questionService;
        }

        private bool IsAuthenticated => User?.Identity != null && User.Identity.IsAuthenticated;

        public async Task<IActionResult> Index(CancellationToken cancel)
        {
            var questions = await _questionService.GetAsync(cancel);

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
            if (IsAuthenticated)
                return RedirectToAction("Admin");
            
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Admin(long filterVariantId, CancellationToken cancel)
        {
            var model = new List<RegistrationModel>();
            
            var registrations = filterVariantId != 0
                ? await _registrationService.GetAsync(filterVariantId, cancel)
                : await _registrationService.GetAsync(cancel);

            foreach (var registration in registrations)
            {
                var registrationModel = new RegistrationModel()
                {
                    FirstName = registration.FirstName,
                    SecondName = registration.SecondName,
                    Phone = registration.Phone,
                    BirthDate = registration.BirthDate,
                    Questions = new List<QuestionModel>()
                };
                
                var variantIdList = registration.Answers
                    .Select(d => d.VariantId)
                    .ToList();
                
                foreach (var variantId in variantIdList)
                {
                    var dbVariant = await _variantService.GetAsync(variantId, cancel);
                    var dbQuestion = await _questionService.GetAsync(dbVariant.QuestionId, cancel);

                    var questionModel = registrationModel.Questions.FirstOrDefault(q => q.Id == dbQuestion.Id);
                    if (questionModel == null)
                    {
                        questionModel = new QuestionModel()
                        {
                            Id = dbQuestion.Id,
                            Name = dbQuestion.Name,
                            Variants = new List<VariantModel>()
                        };
                    
                        registrationModel.Questions.Add(questionModel);
                    }
                
                    questionModel.Variants.Add(new VariantModel()
                    {
                        Id = dbVariant.Id,
                        Name = dbVariant.Name
                    });
                }
                
                model.Add(registrationModel);
            }
            
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch
            {
                // ignored
            }

            if (model.Username == null || model.Password == null)
                return BadRequest("Error");

            if (model.Username != "admin")
                return BadRequest("Error");

            var user = new User()
            {
                Id = 1,
                Name = "admin"
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = Request.Host.Value
            };

            try
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
            catch (Exception)
            {
                // ignored
            }

            return RedirectToAction("Admin");
        }
        
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect(Url.Content("~/"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegistration(RegistrationModel registrationModel,
            CancellationToken cancel)
        {
            if (!ModelState.IsValid)
            {
                var errors = "";
                
                foreach (var value in ModelState.Values)
                foreach (var error in value.Errors)
                    errors += error.ErrorMessage + "</br>";
                
                return Json(errors);
            }

            var registry = new Registration
            {
                FirstName = registrationModel.FirstName,
                SecondName = registrationModel.SecondName,
                Phone = registrationModel.Phone,
                BirthDate = registrationModel.BirthDate
            };

            await _registrationService.Save(registry, cancel);

            foreach (var questionModel in registrationModel.Questions)
            foreach (var variantModel in questionModel.Variants)
            {
                if (variantModel.Checked)
                {
                    var answer = new Answer
                    {
                        RegistrationId = registry.Id,
                        VariantId = variantModel.Id
                    };

                    await _answerService.Save(answer, cancel);
                }
            }

            return Json("Success");
        }
    }
}
