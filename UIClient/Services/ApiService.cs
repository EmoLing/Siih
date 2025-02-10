using DB.Models.Departments;
using DB.Models.Users;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace UIClient.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private CancellationToken _cancellationToken;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _cancellationToken = new CancellationToken();
    }

    public async Task<List<User>> GetUsersAsync() => await _httpClient.GetFromJsonAsync<List<User>>("api/users");

    public async Task<bool> AddUserAsync(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", user, _cancellationToken);
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<List<Department>> GetDepartmentsAsync() => await _httpClient.GetFromJsonAsync<List<Department>>("api/departments");
    public async Task<List<Cabinet>> GetCabinetsAsync() => await _httpClient.GetFromJsonAsync<List<Cabinet>>("api/cabinets");
    public async Task<List<JobTitle>> GetJobTitlesAsync() => await _httpClient.GetFromJsonAsync<List<JobTitle>>("api/jobtitles");
}
