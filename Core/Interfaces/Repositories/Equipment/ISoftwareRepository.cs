using Core.Models.Equipment;

namespace Core.Interfaces.Repositories.Equipment;

public interface ISoftwareRepository
{
    Task<Software> GetSoftwareByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Software>> GetAllSoftwaresAsync(CancellationToken cancellationToken = default);
    Task<Software> AddSoftwareAsync(Software software, CancellationToken cancellationToken = default);
    Task<Software> UpdateSoftwareAsync(Software software, CancellationToken cancellationToken = default);
    Task<bool> DeleteSoftwareAsync(int id, CancellationToken cancellationToken = default);
}
