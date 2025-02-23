using Core.Interfaces.Repositories.Departments;
using Core.Models.Departments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Departments;

internal class DepartmentRepository(ApplicationDBContext dbContext) : IDepartmentRepository
{
    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default)
        => await dbContext.Departments.Include(d => d.Cabinets).ToListAsync(cancellationToken);

    public async Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.Departments.FindAsync([id], cancellationToken);

    public async Task<Department> AddDepartmentAsync(Department department, CancellationToken cancellationToken = default)
    {
        if (department is null)
            return null;

        if (department.Cabinets.Count > 0)
        {
            var existingCabinets = await dbContext.Cabinets.Where(c => department.Cabinets.Contains(c)).ToListAsync(cancellationToken);
            var newExistingCabinets = department.Cabinets.Where(c => !existingCabinets.Contains(c));

            department.Cabinets.Clear();

            if (newExistingCabinets.Any())
                await dbContext.Cabinets.AddRangeAsync(newExistingCabinets, cancellationToken);

            if (existingCabinets.Count > 0)
                department.Cabinets.AddRange(existingCabinets);
        }

        await dbContext.Departments.AddAsync(department, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Departments.FindAsync([department.Id], cancellationToken);
    }

    public async Task<bool> DeleteDepartmentAsync(int id, CancellationToken cancellationToken = default)
    {
        var department = await dbContext.Departments.FindAsync([id], cancellationToken);

        if (department is null)
            return false;

        dbContext.Departments.Remove(department);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Department> UpdateDepartmentAsync(Department department, CancellationToken cancellationToken = default)
    {
        if (department.Cabinets.Count > 0)
        {
            var existingCabinets = await dbContext.Cabinets.Where(c => department.Cabinets.Contains(c)).ToListAsync(cancellationToken);
            var newExistingCabinets = department.Cabinets.Where(c => !existingCabinets.Contains(c));

            department.Cabinets.Clear();

            if (newExistingCabinets.Any())
                await dbContext.Cabinets.AddRangeAsync(newExistingCabinets, cancellationToken);

            if (existingCabinets.Count > 0)
                department.Cabinets.AddRange(existingCabinets);
        }

        dbContext.Departments.Update(department);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Departments.FindAsync([department.Id], cancellationToken);
    }
}
