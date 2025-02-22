using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Departments;

namespace UIClient.Services;

public class DepartmentsApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<DepartmentObject>> GetDepartmentsAsync() => await HttpClient.GetFromJsonAsync<List<DepartmentObject>>("api/departments");

    public async Task<DepartmentObject> AddDepartmentAsync(DepartmentObject department)
    {
        var response = await HttpClient.PostAsJsonAsync("api/departments", department, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdDepartment = await response.Content.ReadFromJsonAsync<DepartmentObject>(CancellationToken);
        return createdDepartment;
    }
}
