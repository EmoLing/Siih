using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Shared.DTOs.Departments;
using Shared.DTOs.Equipment;
using Shared.DTOs.Users;

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

    #region Users
    public async Task<List<UserObject>> GetUsersAsync() => await _httpClient.GetFromJsonAsync<List<UserObject>>("api/users");

    public async Task<UserObject> AddUserAsync(UserObject user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", user, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<UserObject>(_cancellationToken);
        return createdUser;
    }

    public async Task<UserObject> UpdateUserAsync(UserObject user)
    {
        var response = await _httpClient.PutAsJsonAsync("api/hardwares", user, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<UserObject>(_cancellationToken);
        return createdUser;
    }
    #endregion

    #region Departments

    public async Task<List<DepartmentObject>> GetDepartmentsAsync() => await _httpClient.GetFromJsonAsync<List<DepartmentObject>>("api/departments");

    public async Task<DepartmentObject> AddDepartmentAsync(DepartmentObject department)
    {
        var response = await _httpClient.PostAsJsonAsync("api/departments", department, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdDepartment = await response.Content.ReadFromJsonAsync<DepartmentObject>(_cancellationToken);
        return createdDepartment;
    }

    #endregion

    #region Cabinets

    public async Task<List<CabinetObject>> GetCabinetsAsync() => await _httpClient.GetFromJsonAsync<List<CabinetObject>>("api/cabinets");

    public async Task<CabinetObject> AddCabinetAsync(CabinetObject cabinet)
    {
        var response = await _httpClient.PostAsJsonAsync("api/cabinets", cabinet, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdCabinet = await response.Content.ReadFromJsonAsync<CabinetObject>(_cancellationToken);
        return createdCabinet;
    }
    #endregion

    #region JobTitles
    public async Task<List<JobTitleObject>> GetJobTitlesAsync() => await _httpClient.GetFromJsonAsync<List<JobTitleObject>>("api/jobtitles");

    public async Task<JobTitleObject> AddJobTitleAsync(JobTitleObject jobTitle)
    {
        var response = await _httpClient.PostAsJsonAsync("api/jobtitles", jobTitle, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdJobTitle = await response.Content.ReadFromJsonAsync<JobTitleObject>(_cancellationToken);
        return createdJobTitle;
    }
    #endregion

    #region Softwares
    public async Task<List<SoftwareObject>> GetSoftwaresAsync() => await _httpClient.GetFromJsonAsync<List<SoftwareObject>>("api/softwares");

    public async Task<SoftwareObject> AddSoftwareAsync(SoftwareObject software)
    {
        var response = await _httpClient.PostAsJsonAsync("api/softwares", software, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdSoftware = await response.Content.ReadFromJsonAsync<SoftwareObject>(_cancellationToken);
        return createdSoftware;
    }
    #endregion

    #region Hardwares
    public async Task<List<HardwareObject>> GetHardwaresAsync() => await _httpClient.GetFromJsonAsync<List<HardwareObject>>("api/hardwares");

    public async Task<HardwareObject> AddHardwareAsync(HardwareObject hardware)
    {
        var response = await _httpClient.PostAsJsonAsync("api/hardwares", hardware, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdHardware = await response.Content.ReadFromJsonAsync<HardwareObject>(_cancellationToken);
        return createdHardware;
    }

    #endregion

    #region ComplexesHardware
    public async Task<List<ComplexHardwareObject>> GetComplexesHardwareAsync() => await _httpClient.GetFromJsonAsync<List<ComplexHardwareObject>>("api/complexeshardware");

    public async Task<ComplexHardwareObject> AddComplexHardwareAsync(ComplexHardwareObject complexHardware)
    {
        var response = await _httpClient.PostAsJsonAsync("api/complexeshardware", complexHardware, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdComplexHardware = await response.Content.ReadFromJsonAsync<ComplexHardwareObject>(_cancellationToken);
        return createdComplexHardware;
    }
    #endregion
}
