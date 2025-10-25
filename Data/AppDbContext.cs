using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using R_TUT.Models;

namespace R_TUT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<R_TUT.Models.DOCUMENTOS> DOCUMENTOS { get; set; } = default!;
    }
}
