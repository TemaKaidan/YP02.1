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
    public class ConsultationsContext : DbContext
    {
        public DbSet<Consultations> Consultations { get; set; }
        public ConsultationsContext()
        {
            Database.EnsureCreated();
            Consultations.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}