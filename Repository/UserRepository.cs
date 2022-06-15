using MessagingService.Data;
using MessagingService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingService.Repository
{
    public class UserRepository : IRepository<User>
    {
        MessagingServiceContext dataContext;

        public UserRepository(MessagingServiceContext context)
        {
            dataContext = context;
        }

        public async Task<User> GetById(int? id)
        {
            return await dataContext.User
                .Where(u => u.Id.Equals(id))
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetByName(string? name)
        {
            return await dataContext.User
                .Where(u => u.Name.Equals(name))
                .SingleOrDefaultAsync();
        }

        public async Task<IList<User>> GetList()
        {
            return await dataContext.User.OrderBy(u => u.Id).ToListAsync();
        }

        public async Task Register(User user)
        {
            using (dataContext)
            {
                await dataContext.AddAsync(user);
                await dataContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (dataContext)
            {
                User userToDelete = dataContext.User.Find(id);

                if (userToDelete != null)
                    dataContext.User.Remove(userToDelete);
                await dataContext.SaveChangesAsync();
            }
        }

        public async Task Update(User updatedUser, int id)
        {
            using (dataContext)
            {
                User oldUser = dataContext.User.Find(id);
                if (oldUser != null)
                {
                    oldUser.Name = updatedUser.Name;
                    oldUser.Email = updatedUser.Email;
                    dataContext.Update(oldUser);
                }

                await dataContext.SaveChangesAsync();
            }
        }
    }
}
