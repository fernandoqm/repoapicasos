using Microsoft.EntityFrameworkCore;
using TarjetasApi.Domain.Entities;

namespace TarjetasApi.Application.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<tarjeta> Tarjetas { get; set; }

    }
}
