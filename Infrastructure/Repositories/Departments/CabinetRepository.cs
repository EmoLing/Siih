using Core.Interfaces.Repositories.Departments;
using Core.Models.Departments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Departments;

internal class CabinetRepository(ApplicationDBContext dbContext) : ICabinetRepository
{
    public async Task<IEnumerable<Cabinet>> GetAllCabinetsAsync(CancellationToken cancellationToken = default)
        => await dbContext.Cabinets.Include(c => c.Department).ToListAsync(cancellationToken);

    public async Task<Cabinet> GetCabinetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.Cabinets.FindAsync([id], cancellationToken);

    public async Task<Cabinet> AddCabinetAsync(Cabinet cabinet, CancellationToken cancellationToken = default)
    {
        if (cabinet is null)
            return null;

        if (cabinet.Department is not null)
        {
            var existingDepartment = await dbContext.Departments.FindAsync([cabinet.Department.Id], cancellationToken);

            if (existingDepartment is null)
                await dbContext.Departments.AddAsync(cabinet.Department, cancellationToken);
            else
                cabinet.Department = existingDepartment;
        }

        await dbContext.Cabinets.AddAsync(cabinet, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Cabinets.FindAsync([cabinet.Id], cancellationToken);
    }

    public async Task<bool> DeleteCabinetAsync(int id, CancellationToken cancellationToken = default)
    {
        var cabinet = await dbContext.Cabinets.FindAsync([id], cancellationToken);

        if (cabinet is null)
            return false;

        dbContext.Cabinets.Remove(cabinet);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Cabinet> UpdateCabinetAsync(Cabinet cabinet, CancellationToken cancellationToken = default)
    {
        if (cabinet.Department is not null)
        {
            var existingDepartment = await dbContext.Departments.FindAsync([cabinet.Department.Id], cancellationToken);

            if (existingDepartment is null)
                await dbContext.Departments.AddAsync(cabinet.Department, cancellationToken);
            else
                cabinet.Department = existingDepartment;
        }

        dbContext.Cabinets.Update(cabinet);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Cabinets.FindAsync([cabinet.Id], cancellationToken);
    }
}
