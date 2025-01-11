using DB.Models.Departments;
using DB.Models.Equipment;
using DB.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DB;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Software> Softwares { get; set; } = null!;
    public DbSet<Hardware> Hardwares { get; set; } = null!;
    public DbSet<ComplexHardware> ComplexesHardware { get; set; } = null!;
    public DbSet<Department> Department { get; set; } = null!;
    public DbSet<Cabinet> Cabinets { get; set; } = null!;
    public DbSet<JobTitle> JobTitles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);
    }
}
