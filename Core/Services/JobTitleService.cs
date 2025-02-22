using Core.Interfaces.Repositories.Users;
using Core.Models.Users;

namespace Core.Services;

public class JobTitleService(IJobTitleRepository jobTitleRepository)
{
    public async Task<JobTitle> GetJobTitleByIdAsync(int id, CancellationToken cancellationToken = default)
        => await jobTitleRepository.GetJobTitleByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(CancellationToken cancellationToken = default)
        => await jobTitleRepository.GetAllJobTitlesAsync(cancellationToken);

    public async Task<JobTitle> AddJobTitleAsync(JobTitle jobTitle, CancellationToken cancellationToken = default)
        => await jobTitleRepository.AddJobTitleAsync(jobTitle, cancellationToken);

    public async Task<JobTitle> UpdateJobTitleAsync(JobTitle jobTitle, CancellationToken cancellationToken = default)
        => await jobTitleRepository.UpdateJobTitleAsync(jobTitle, cancellationToken);

    public async Task<bool> DeleteJobTitleAsync(int id, CancellationToken cancellationToken = default)
        => await jobTitleRepository.DeleteJobTitleAsync(id, cancellationToken);
}
