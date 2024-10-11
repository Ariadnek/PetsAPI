using Microsoft.EntityFrameworkCore;
using PetsApi.Models;

namespace PetsApi.Data
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options) { }

        public DbSet<Animal> Animais { get; set; }
    }
}
