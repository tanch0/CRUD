using System;
using Microsoft.EntityFrameworkCore;
using CrudDemo.Models;

namespace CrudDemo.Data
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee>Employess { get; set; }
    }
}

