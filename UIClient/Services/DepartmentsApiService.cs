using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Departments;

namespace UIClient.Services;

public class DepartmentsApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<DepartmentObject>> GetDepartmentsAsync() => await HttpClient.GetFromJsonAsync<List<DepartmentObject>>("api/departments");

    public async Task<DepartmentObject> GetDepartmentAsync(int id) => await HttpClient.GetFromJsonAsync<DepartmentObject>($"api/departments/{id}");

    public async Task<DepartmentObject> AddDepartmentAsync(DepartmentObject department)
    {
        var response = await HttpClient.PostAsJsonAsync("api/departments", department, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdDepartment = await response.Content.ReadFromJsonAsync<DepartmentObject>(CancellationToken);
        return createdDepartment;
    }

    public async Task UpdateDepartmentAsync(DepartmentObject department)
    {
        var response = await HttpClient.PutAsJsonAsync("api/departments", department, CancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"api/departments/{id}", CancellationToken);
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
