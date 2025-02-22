using Core.Models.Users;

namespace Core.Interfaces.Repositories.Users;

public interface IJobTitleRepository
{
    Task<JobTitle> GetJobTitleByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<JobTitle>> GetAllJobTitlesAsync(CancellationToken cancellationToken = default);
    Task<JobTitle> AddJobTitleAsync(JobTitle jobTitle, CancellationToken cancellationToken = default);
    Task<JobTitle> UpdateJobTitleAsync(JobTitle jobTitle, CancellationToken cancellationToken = default);
    Task<bool> DeleteJobTitleAsync(int id, CancellationToken cancellationToken = default);
}
