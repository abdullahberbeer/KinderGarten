using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Data
{
    public class KinderGartenContext:IdentityDbContext<User,Role,int>
    {
        public KinderGartenContext(DbContextOptions<KinderGartenContext> options):base(options)
        {
            
        }

        public DbSet<Classes> Classess { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Classes> Classes { get; set; }
    }
}