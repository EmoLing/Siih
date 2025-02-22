using Core.Interfaces.Repositories.Equipment;
using Core.Models.Equipment;

namespace Core.Services;

public class ComplexHardwareService(IComplexHardwareRepository complexRepository)
{
    public async Task<ComplexHardware> GetComplexHardwareByIdAsync(int id, CancellationToken cancellationToken = default)
        => await complexRepository.GetComplexHardwareByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<ComplexHardware>> GetAllComplexesHardwareAsync(CancellationToken cancellationToken = default)
        => await complexRepository.GetAllComplexesHardwareAsync(cancellationToken);

    public async Task<ComplexHardware> AddComplexHardwareAsync(ComplexHardware complexHardware, CancellationToken cancellationToken = default)
        => await complexRepository.AddComplexHardwareAsync(complexHardware, cancellationToken);

    public async Task<ComplexHardware> UpdateComplexHardwareAsync(ComplexHardware complexHardware, CancellationToken cancellationToken = default)
        => await complexRepository.UpdateComplexHardwareAsync(complexHardware, cancellationToken);

    public async  Task<bool> DeleteComplexHardwareAsync(int id, CancellationToken cancellationToken = default)
        => await complexRepository.DeleteComplexHardwareAsync(id, cancellationToken);
}
