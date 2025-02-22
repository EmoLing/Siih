using Core.Interfaces.Repositories.Equipment;
using Core.Models.Equipment;

namespace Core.Services;

public class SoftwareService(ISoftwareRepository softwareRepository)
{
    public async Task<Software> GetSoftwareByIdAsync(int id, CancellationToken cancellationToken = default)
        => await softwareRepository.GetSoftwareByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<Software>> GetAllSoftwaresAsync(CancellationToken cancellationToken = default)
        => await softwareRepository.GetAllSoftwaresAsync(cancellationToken);

    public async Task<Software> AddSoftwareAsync(Software software, CancellationToken cancellationToken = default)
        => await softwareRepository.AddSoftwareAsync(software, cancellationToken);

    public async Task<Software> UpdateSoftwareAsync(Software software, CancellationToken cancellationToken = default)
        => await softwareRepository.UpdateSoftwareAsync(software, cancellationToken);

    public async Task<bool> DeleteSoftwareAsync(int id, CancellationToken cancellationToken = default)
        => await softwareRepository.DeleteSoftwareAsync(id, cancellationToken);
}
