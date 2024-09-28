using LoginAuth.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoginAuth.Data
{
    public class PgDbContext : DbContext
    {
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}

