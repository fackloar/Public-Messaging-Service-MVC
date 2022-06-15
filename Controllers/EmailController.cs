
using MessagingService.Models;
using MessagingService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MessagingService.Controllers
{
    public class EmailController : Controller
    {
        private readonly IRepository<User> _repository;

        public EmailController(IRepository<User> repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Send()
        {
            if (ModelState.IsValid)
            {
                EmailService emailService = new EmailService();
                IList<User> listOfUsers = new List<User>();
                foreach (User user in _repository.GetList().Result)
                {
                    listOfUsers.Add(user);
                }
                foreach (User user in listOfUsers)
                {
                    var email = new Email();
                    email.Subject = TemplateCreator.GetEmailSubject();
                    email.Message = await TemplateCreator.GetEmailMessage(user.Name);
                    await emailService.SendEmailAsync(user.Email, email.Subject, email.Message);
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            return View("Index");
        }
    }
}
