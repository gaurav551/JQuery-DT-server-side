using JQueryDT.Models;
using Microsoft.EntityFrameworkCore;

namespace JQueryDT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        
    }
}