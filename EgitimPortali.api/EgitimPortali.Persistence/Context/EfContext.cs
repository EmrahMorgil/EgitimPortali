using EgitimPortali.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EgitimPortali.Persistence.Context
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<UserCourse> UserCourse { get; set; }
        public DbSet<Documentation> Documentation { get; set; }
    }
}
