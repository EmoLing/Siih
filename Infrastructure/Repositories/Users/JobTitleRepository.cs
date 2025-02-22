using Core.Interfaces.Repositories.Users;
using Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

internal class JobTitleRepository(ApplicationDBContext dbContext) : IJobTitleRepository
{
    public async Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(CancellationToken cancellationToken = default)
        => await dbContext.JobTitles.ToListAsync(cancellationToken);

    public async Task<JobTitle> GetJobTitleByIdAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.JobTitles.FindAsync([id], cancellationToken);

    public async Task<JobTitle> AddJobTitleAsync(JobTitle jobTitle, CancellationToken cancellationToken = default)
    {
        await dbContext.JobTitles.AddAsync(jobTitle, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.JobTitles.FindAsync([jobTitle.Id], cancellationToken);
    }

    public async Task<bool> DeleteJobTitleAsync(int id, CancellationToken cancellationToken = default)
    {
        var jobTitle = await dbContext.JobTitles.FindAsync([id], cancellationToken);

        if (jobTitle is null)
            return false;

        dbContext.JobTitles.Remove(jobTitle);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<JobTitle> UpdateJobTitleAsync(JobTitle jobTitle, CancellationToken cancellationToken = default)
    {
        dbContext.JobTitles.Update(jobTitle);
        await dbContext.SaveChangesAsync(cancellationToken);

        return jobTitle;
    }
}
