using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FakeNameContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=GIGABYTE\SQLEXPRESS;Database=FakeNames;Trusted_Connection=true");
        }
        public DbSet<User> Users { get; set; }
    }
}
