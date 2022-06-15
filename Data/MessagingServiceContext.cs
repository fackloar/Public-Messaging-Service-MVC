#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MessagingService.Models;

namespace MessagingService.Data
{
    public class MessagingServiceContext : DbContext
    {
        public MessagingServiceContext (DbContextOptions<MessagingServiceContext> options)
            : base(options)
        {
        }

        public DbSet<MessagingService.Models.User> User { get; set; }
    }
}
