using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YP02.Context.Database;
using YP02.Models;

namespace YP02.Context
{
    public class TeachersLoadContext : DbContext
    {
        public DbSet<TeachersLoad> TeachersLoad { get; set; }
        public TeachersLoadContext()
        {
            Database.EnsureCreated();
            TeachersLoad.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}