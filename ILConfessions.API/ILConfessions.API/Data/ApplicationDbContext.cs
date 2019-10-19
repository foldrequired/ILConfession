using System;
using System.Collections.Generic;
using System.Text;
using ILConfessions.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ILConfessions.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Confession> Confessions { get; set; } 

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
