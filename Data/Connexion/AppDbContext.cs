using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TestApplication.Data.Models;

namespace TestApplication.Data.Connexion
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonSup40> PersonsSup40s { get; set; }
    }
}
