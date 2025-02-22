using Core.Models.Departments;

namespace Core.Interfaces.Repositories.Departments;

public interface IDepartmentRepository
{
    Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default);
    Task<Department> AddDepartmentAsync(Department department, CancellationToken cancellationToken = default);
    Task<Department> UpdateDepartmentAsync(Department department, CancellationToken cancellationToken = default);
    Task<bool> DeleteDepartmentAsync(int id, CancellationToken cancellationToken = default);
}
