using Core.Models;
using Core.Models.Departments;
using Core.Models.Equipment;
using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public DbSet<Software> Softwares { get; set; } = null!;
    public DbSet<Hardware> Hardwares { get; set; } = null!;
    public DbSet<ComplexHardware> ComplexesHardware { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Cabinet> Cabinets { get; set; } = null!;
    public DbSet<JobTitle> JobTitles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

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
