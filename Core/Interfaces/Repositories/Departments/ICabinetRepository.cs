using Core.Models.Departments;

namespace Core.Interfaces.Repositories.Departments;

public interface ICabinetRepository
{
    Task<Cabinet> GetCabinetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Cabinet>> GetAllCabinetsAsync(CancellationToken cancellationToken = default);
    Task<Cabinet> AddCabinetAsync(Cabinet cabinet, CancellationToken cancellationToken = default);
    Task<Cabinet> UpdateCabinetAsync(Cabinet cabinet, CancellationToken cancellationToken = default);
    Task<bool> DeleteCabinetAsync(int id, CancellationToken cancellationToken = default);
}
