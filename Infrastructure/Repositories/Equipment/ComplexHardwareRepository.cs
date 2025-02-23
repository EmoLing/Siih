using Core.Interfaces.Repositories.Equipment;
using Core.Models.Equipment;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Equipment;

internal class ComplexHardwareRepository(ApplicationDBContext dbContext) : IComplexHardwareRepository
{
    public async Task<IEnumerable<ComplexHardware>> GetAllComplexesHardwareAsync(CancellationToken cancellationToken = default)
        => await dbContext.ComplexesHardware.Include(ch => ch.Hardwares).Include(ch => ch.User).ToListAsync(cancellationToken);

    public async Task<ComplexHardware> GetComplexHardwareByIdAsync(int id, CancellationToken cancellationToken = default)
        => await (dbContext.ComplexesHardware.Include(ch => ch.Hardwares).Include(ch => ch.User).Include(ch => ch.User.Cabinet))
        .FirstOrDefaultAsync(ch => ch.Id == id, cancellationToken);

    public async Task<ComplexHardware> AddComplexHardwareAsync(ComplexHardware complexHardware, CancellationToken cancellationToken = default)
    {
        if (complexHardware.User is not null)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(ch => ch.Id == complexHardware.User.Id, cancellationToken);

            if (existingUser is null)
                dbContext.Users.Add(complexHardware.User);
            else
                complexHardware.User = existingUser;
        }

        if (complexHardware.Hardwares.Count > 0)
        {
            var existingHardwares = await dbContext.Hardwares.Where(h => complexHardware.Hardwares.Contains(h)).ToListAsync(cancellationToken);
            var newExistingHardwares = complexHardware.Hardwares.Where(h => !existingHardwares.Contains(h));

            complexHardware.Hardwares.Clear();

            if (newExistingHardwares.Any())
                await dbContext.Hardwares.AddRangeAsync(newExistingHardwares, cancellationToken);

            if (existingHardwares.Count != 0)
                complexHardware.Hardwares.AddRange(existingHardwares);
        }

        await dbContext.ComplexesHardware.AddAsync(complexHardware, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.ComplexesHardware.FindAsync([complexHardware.Id], cancellationToken);
    }

    public async Task<bool> DeleteComplexHardwareAsync(int id, CancellationToken cancellationToken = default)
    {
        var complexesHardware = await dbContext.ComplexesHardware.FindAsync([id], cancellationToken);

        if (complexesHardware is null)
            return false;

        dbContext.ComplexesHardware.Remove(complexesHardware);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<ComplexHardware> UpdateComplexHardwareAsync(ComplexHardware complexHardware, CancellationToken cancellationToken = default)
    {
        dbContext.ComplexesHardware.Update(complexHardware);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await GetComplexHardwareByIdAsync(complexHardware.Id, cancellationToken);
    }
}
