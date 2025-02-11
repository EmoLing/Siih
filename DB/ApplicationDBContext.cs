using DB.Models;
using DB.Models.Departments;
using DB.Models.Equipment;
using DB.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DB;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public DbSet<Software> Softwares { get; set; } = null!;
    public DbSet<Hardware> Hardwares { get; set; } = null!;
    public DbSet<ComplexHardware> ComplexesHardware { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Cabinet> Cabinets { get; set; } = null!;
    public DbSet<JobTitle> JobTitles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();

        string currentDirectory = Directory.GetCurrentDirectory();
        builder.SetBasePath(currentDirectory);
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString).LogTo(Console.WriteLine, LogLevel.Warning);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatabaseObject>()
            .Property(o => o.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<DatabaseObject>()
            .Property(o => o.Guid)
            .HasValueGenerator<GuidValueGenerator>();

        modelBuilder.Entity<Software>().ToTable(nameof(Software));
        modelBuilder.Entity<Hardware>().ToTable(nameof(Hardware));
        modelBuilder.Entity<ComplexHardware>().ToTable(nameof(ComplexHardware));
        modelBuilder.Entity<Department>().ToTable(nameof(Department));
        modelBuilder.Entity<Cabinet>().ToTable(nameof(Cabinet));
        modelBuilder.Entity<JobTitle>().ToTable(nameof(JobTitle));
        modelBuilder.Entity<User>().ToTable(nameof(User));
    }
}
