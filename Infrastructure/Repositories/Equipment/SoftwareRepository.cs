using Core.Interfaces.Repositories.Equipment;
using Core.Models.Departments;
using Core.Models.Equipment;
using Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Equipment;

internal class SoftwareRepository(ApplicationDBContext dbContext) : ISoftwareRepository
{
    public async Task<IEnumerable<Software>> GetAllSoftwaresAsync(CancellationToken cancellationToken = default)
        => await dbContext.Softwares.Include(s => s.Hardwares).ToListAsync(cancellationToken);

    public async Task<Software> GetSoftwareByIdAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.Softwares.FindAsync([id], cancellationToken);

    public async Task<Software> AddSoftwareAsync(Software software, CancellationToken cancellationToken = default)
    {
        if (software.Hardwares.Count > 0)
        {
            var existingHardwares = await dbContext.Hardwares.Where(h => software.Hardwares.Contains(h)).ToListAsync(cancellationToken);
            var newExistingHardwares = software.Hardwares.Where(h => !existingHardwares.Contains(h));

            software.Hardwares.Clear();

            if (newExistingHardwares.Any())
                await dbContext.Hardwares.AddRangeAsync(newExistingHardwares, cancellationToken);

            if (existingHardwares.Count > 0)
                software.Hardwares.AddRange(existingHardwares);
        }

        await dbContext.Softwares.AddAsync(software, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Softwares.FindAsync([software.Id], cancellationToken);
    }

    public async Task<bool> DeleteSoftwareAsync(int id, CancellationToken cancellationToken = default)
    {
        var software = await dbContext.Softwares.FindAsync([id], cancellationToken);

        if (software is null)
            return false;

        dbContext.Softwares.Remove(software);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Software> UpdateSoftwareAsync(Software software, CancellationToken cancellationToken = default)
    {
        if (software.Hardwares.Count > 0)
        {
            var existingHardwares = await dbContext.Hardwares.Where(h => software.Hardwares.Contains(h)).ToListAsync(cancellationToken);
            var newExistingHardwares = software.Hardwares.Where(h => !existingHardwares.Contains(h));

            software.Hardwares.Clear();

            if (newExistingHardwares.Any())
                await dbContext.Hardwares.AddRangeAsync(newExistingHardwares, cancellationToken);

            if (existingHardwares.Count > 0)
                software.Hardwares.AddRange(existingHardwares);
        }

        dbContext.Softwares.Update(software);
        await dbContext.SaveChangesAsync(cancellationToken);

        return software;
    }
}
