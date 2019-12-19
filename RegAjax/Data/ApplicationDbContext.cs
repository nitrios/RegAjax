using Microsoft.EntityFrameworkCore;
using RegAjax.Data.Entities;
using RegAjax.Data.InitData;

namespace RegAjax.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        
        public virtual DbSet<Question> Questions { get; set; }
        
        public virtual DbSet<Registration> Registrations { get; set; }
        
        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Variant> Variants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasData(QuestionsInitData.Get());
            
            modelBuilder.Entity<User>()
                .HasData(UsersInitData.Get());
            
            modelBuilder.Entity<Variant>()
                .HasData(VariantsInitData.Get());
        }
    }
}