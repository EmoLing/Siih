using Core.Interfaces.Repositories.Departments;
using Core.Models.Departments;

namespace Core.Services;

public class DepartmentService(IDepartmentRepository departmentRepository)
{
    public async Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default)
        => await departmentRepository.GetDepartmentByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken = default)
        => await departmentRepository.GetAllDepartmentsAsync(cancellationToken);

    public async Task<Department> AddDepartmentAsync(Department department, CancellationToken cancellationToken = default)
        => await departmentRepository.AddDepartmentAsync(department, cancellationToken);

    public async Task<Department> UpdateDepartmentAsync(Department department, CancellationToken cancellationToken = default)
        => await departmentRepository.UpdateDepartmentAsync(department, cancellationToken);

    public async Task<bool> DeleteDepartmentAsync(int id, CancellationToken cancellationToken = default)
        => await departmentRepository.DeleteDepartmentAsync(id, cancellationToken);
}
