using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeC
{
    public class MyContext : DbContext
    {
        public DbSet<RoboAlura> RoboAlura { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
             => options.UseSqlite(@"Data Source=C:\sqllite\AeC.db");

    }
}
