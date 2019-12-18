using Microsoft.EntityFrameworkCore;
using RegAjax.Data.Entities;

namespace RegAjax.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Registration> Registrations { get; set; }
    }
}