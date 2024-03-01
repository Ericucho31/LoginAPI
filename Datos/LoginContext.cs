using Microsoft.EntityFrameworkCore;
using Entidades;
using Datos.Mapping;

namespace Datos
{
    public class LoginContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public LoginContext() { }
        public LoginContext(DbContextOptions options) : base(options) 
        { 
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("LoginConnection");
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmailMapping());
            modelBuilder.ApplyConfiguration(new PasswordMapping());
        }*/
    }
}
