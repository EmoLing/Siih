using Core.Interfaces.Repositories.Departments;
using Core.Models.Departments;

namespace Core.Services;

public class CabinetService(ICabinetRepository cabinetRepository)
{
    public async Task<Cabinet> GetCabinetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await cabinetRepository.GetCabinetByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<Cabinet>> GetAllCabinetsAsync(CancellationToken cancellationToken = default)
        => await cabinetRepository.GetAllCabinetsAsync(cancellationToken);

    public async Task<Cabinet> AddCabinetAsync(Cabinet cabinet, CancellationToken cancellationToken = default)
        => await cabinetRepository.AddCabinetAsync(cabinet, cancellationToken);

    public async Task<Cabinet> UpdateCabinetAsync(Cabinet cabinet, CancellationToken cancellationToken = default)
        => await cabinetRepository.UpdateCabinetAsync(cabinet, cancellationToken);

    public async Task<bool> DeleteCabinetAsync(int id, CancellationToken cancellationToken = default)
        => await cabinetRepository.DeleteCabinetAsync(id, cancellationToken);
}
