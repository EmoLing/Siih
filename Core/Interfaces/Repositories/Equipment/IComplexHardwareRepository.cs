using Core.Models.Equipment;

namespace Core.Interfaces.Repositories.Equipment;

public interface IComplexHardwareRepository
{
    Task<ComplexHardware> GetComplexHardwareByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ComplexHardware>> GetAllComplexesHardwareAsync(CancellationToken cancellationToken = default);
    Task<ComplexHardware> AddComplexHardwareAsync(ComplexHardware complexHardware, CancellationToken cancellationToken = default);
    Task<ComplexHardware> UpdateComplexHardwareAsync(ComplexHardware complexHardware, CancellationToken cancellationToken = default);
    Task<bool> DeleteComplexHardwareAsync(int id, CancellationToken cancellationToken = default);
}
