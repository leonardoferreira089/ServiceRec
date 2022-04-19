using Microsoft.EntityFrameworkCore;
using ServiceRec.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRec.Data.Context
{
    public class RecDbContext : DbContext
    {
        public RecDbContext(DbContextOptions<RecDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set;}
        public DbSet<Client> Clients { get; set; }
    }
}
