using MessagingService.Controllers;
using MessagingService.Models;
using MessagingService.Repository;
using Quartz;

namespace MessagingService.Scheduler
{
    public class SendMailJob : IJob
    {
        private readonly IRepository<User> _repository;
        public SendMailJob(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            EmailService emailService = new EmailService();
            List<User> listOfUsers = new List<User>();
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
    }
}
