using Core.Interfaces.Repositories.Users;
using Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class UserRepository(ApplicationDBContext dbContext) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default) => await dbContext.Users
        .Include(u => u.JobTitle)
        .Include(u => u.Cabinet).ThenInclude(c => c.Department)
        .Include(u => u.ComplexHardwares).ToListAsync(cancellationToken);

    public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.Users.FindAsync([id], cancellationToken);

    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user.JobTitle is not null)
        {
            var existingJobTitle = await dbContext.JobTitles.FirstOrDefaultAsync(jt => jt.Id == user.JobTitle.Id, cancellationToken);

            if (existingJobTitle is null)
                dbContext.JobTitles.Add(user.JobTitle);
            else
                user.JobTitle = existingJobTitle;
        }

        if (user.Cabinet is not null)
        {
            var existingCabinet = await dbContext.Cabinets.FirstOrDefaultAsync(c => c.Id == user.Cabinet.Id, cancellationToken);

            if (existingCabinet is null)
                dbContext.Cabinets.Add(user.Cabinet);
            else
                user.Cabinet = existingCabinet;
        }

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Users.FindAsync([user.Id], cancellationToken);
    }

    public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.FindAsync([id], cancellationToken);

        if (user is null)
            return false;

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user.JobTitle is not null)
        {
            var existingJobTitle = await dbContext.JobTitles.FirstOrDefaultAsync(jt => jt.Id == user.JobTitle.Id, cancellationToken);

            if (existingJobTitle is null)
                dbContext.JobTitles.Add(user.JobTitle);
            else
                user.JobTitle = existingJobTitle;
        }

        if (user.Cabinet is not null)
        {
            var existingCabinet = await dbContext.Cabinets.FirstOrDefaultAsync(c => c.Id == user.Cabinet.Id, cancellationToken);

            if (existingCabinet is null)
                dbContext.Cabinets.Add(user.Cabinet);
            else
                user.Cabinet = existingCabinet;
        }

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Users.FindAsync([user.Id], cancellationToken);
    }
}
