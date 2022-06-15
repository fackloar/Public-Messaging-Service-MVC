using MessagingService.Data;

namespace MessagingService.Models
{
    public class SeedData
    {
        public static void Initialize(MessagingServiceContext context)
        {
            context.Database.EnsureCreated();

            if (context.User.Any())
            {
                return; //DB has been seeded
            }

            var users = new List<User>()
            {
                new User { Email = "fackloar@gmail.com", Name = "Roman" },
                new User { Email = "john@doe.com", Name = "John Doe"},
                new User { Email = "ivan.ivanovich@rusmail.ru", Name = "Ivan"}
            };

            foreach (User user in users)
            {
                context.User.Add(user);
            }
            context.SaveChanges();

        }
    }
}
