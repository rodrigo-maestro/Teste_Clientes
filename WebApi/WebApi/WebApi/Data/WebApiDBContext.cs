using Microsoft.EntityFrameworkCore;
using WebApi.Data.Map;
using WebApi.Models;

namespace WebApi.Data
{
    public class WebApiDBContext : DbContext
    {
        public WebApiDBContext(DbContextOptions<WebApiDBContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Logradouro> Logradouros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new LogradouroMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
