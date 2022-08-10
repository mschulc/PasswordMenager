using Microsoft.EntityFrameworkCore;
using PasswordMenager.Entities;
using PasswordMenager.Resources;

namespace PasswordMenager.Services;

public class PasswordsDbContext : DbContext
{
    private readonly string _connectionString = Resource.ConnectionString;

    public DbSet<User>? Users { get; set; }
    public DbSet<Passwords>? Passwords { get; set; }

    public void Create()
    {
        if(!Database.CanConnect())
        {
            Database.EnsureCreated();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Passwords>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }
}