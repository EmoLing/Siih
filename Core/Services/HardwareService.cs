using Core.Interfaces.Repositories.Equipment;
using Core.Models.Equipment;

namespace Core.Services;

public class HardwareService(IHardwareRepository hardwareRepository)
{
    public async Task<Hardware> GetHardwareByIdAsync(int id, CancellationToken cancellationToken = default)
        => await hardwareRepository.GetHardwareByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<Hardware>> GetAllHardwaresAsync(CancellationToken cancellationToken = default)
        => await hardwareRepository.GetAllHardwaresAsync(cancellationToken);

    public async Task<Hardware> AddHardwareAsync(Hardware hardware, CancellationToken cancellationToken = default)
        => await hardwareRepository.AddHardwareAsync(hardware, cancellationToken);

    public async Task<Hardware> UpdateHardwareAsync(Hardware hardware, CancellationToken cancellationToken = default)
        => await hardwareRepository.UpdateHardwareAsync(hardware, cancellationToken);

    public async Task<bool> DeleteHardwareAsync(int id, CancellationToken cancellationToken = default)
        => await hardwareRepository.DeleteHardwareAsync(id, cancellationToken);
}
