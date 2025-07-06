using Core_MyProject_Api.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core_MyProject_Api.DAL.ApiContext
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server= DESKTOP-SCO6T6L; database=CoreProjeDB2; integrated security=true; TrustServerCertificate=True;");
        }
        public DbSet<Category> Categories { get; set; }
    }
}
