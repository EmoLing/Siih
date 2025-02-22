using Core.Models.Equipment;

namespace Core.Interfaces.Repositories.Equipment;

public interface IHardwareRepository
{
    Task<Hardware> GetHardwareByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Hardware>> GetAllHardwaresAsync(CancellationToken cancellationToken = default);
    Task<Hardware> AddHardwareAsync(Hardware hardware, CancellationToken cancellationToken = default);
    Task<Hardware> UpdateHardwareAsync(Hardware hardware, CancellationToken cancellationToken = default);
    Task<bool> DeleteHardwareAsync(int id, CancellationToken cancellationToken = default);
}
