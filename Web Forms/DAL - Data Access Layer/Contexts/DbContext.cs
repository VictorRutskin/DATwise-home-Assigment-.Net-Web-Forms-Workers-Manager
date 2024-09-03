using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;

namespace DAL.DbContext
{
    public class myDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
