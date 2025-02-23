using Core.Interfaces.Repositories.Equipment;
using Core.Models.Equipment;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Equipment;

internal class HardwareRepository(ApplicationDBContext dbContext) : IHardwareRepository
{
    public async Task<IEnumerable<Hardware>> GetAllHardwaresAsync(CancellationToken cancellationToken = default)
        => await dbContext.Hardwares.Include(h => h.Softwares).Include(h => h.ComplexHardware).ToListAsync(cancellationToken);

    public async Task<Hardware> GetHardwareByIdAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.Hardwares.Include(h => h.Softwares).Include(h => h.ComplexHardware).FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

    public async Task<Hardware> AddHardwareAsync(Hardware hardware, CancellationToken cancellationToken = default)
    {
        if (hardware.ComplexHardware is not null)
        {
            var existingComplexHardware = await dbContext.ComplexesHardware.FirstOrDefaultAsync(ch => ch.Id == hardware.ComplexHardware.Id, cancellationToken);

            if (existingComplexHardware is null)
                dbContext.ComplexesHardware.Add(hardware.ComplexHardware);
            else
                hardware.ComplexHardware = existingComplexHardware;
        }

        if (hardware.Softwares.Count > 0)
        {
            var existingSoftwares = await dbContext.Softwares.Where(s => hardware.Softwares.Contains(s)).ToListAsync(cancellationToken);
            var newExistingSoftwares = hardware.Softwares.Where(s => !existingSoftwares.Contains(s));

            hardware.Softwares.Clear();

            if (newExistingSoftwares.Any())
                await dbContext.Softwares.AddRangeAsync(newExistingSoftwares, cancellationToken);

            if (existingSoftwares.Count != 0)
                hardware.Softwares.AddRange(existingSoftwares);
        }

        await dbContext.Hardwares.AddAsync(hardware, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Hardwares.FindAsync([hardware.Id], cancellationToken);
    }

    public async Task<bool> DeleteHardwareAsync(int id, CancellationToken cancellationToken = default)
    {
        var hardware = await dbContext.Hardwares.FindAsync([id], cancellationToken);

        if (hardware is null)
            return false;

        dbContext.Hardwares.Remove(hardware);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Hardware> UpdateHardwareAsync(Hardware hardware, CancellationToken cancellationToken = default)
    {
        if (hardware.ComplexHardware is not null)
        {
            var existingComplexHardware = await dbContext.ComplexesHardware.FirstOrDefaultAsync(ch => ch.Id == hardware.ComplexHardware.Id, cancellationToken);

            if (existingComplexHardware is null)
                dbContext.ComplexesHardware.Add(hardware.ComplexHardware);
            else
                hardware.ComplexHardware = existingComplexHardware;
        }

        if (hardware.Softwares.Count > 0)
        {
            var existingSoftwares = await dbContext.Softwares.Where(s => hardware.Softwares.Contains(s)).ToListAsync(cancellationToken);
            var newExistingSoftwares = hardware.Softwares.Where(s => !existingSoftwares.Contains(s));

            hardware.Softwares.Clear();

            if (newExistingSoftwares.Any())
                await dbContext.Softwares.AddRangeAsync(newExistingSoftwares, cancellationToken);

            if (existingSoftwares.Count != 0)
                hardware.Softwares.AddRange(existingSoftwares);
        }

        dbContext.Hardwares.Update(hardware);
        await dbContext.SaveChangesAsync(cancellationToken);

        return hardware;
    }
}
