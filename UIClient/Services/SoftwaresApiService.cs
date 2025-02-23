using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Equipment;

namespace UIClient.Services;

public class SoftwaresApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<SoftwareObject>> GetSoftwaresAsync() => await HttpClient.GetFromJsonAsync<List<SoftwareObject>>("api/softwares");

    public async Task<SoftwareObject> GetSoftwareAsync(int id) => await HttpClient.GetFromJsonAsync<SoftwareObject>($"api/softwares/{id}");

    public async Task<SoftwareObject> AddSoftwareAsync(SoftwareObject software)
    {
        var response = await HttpClient.PostAsJsonAsync("api/softwares", software, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdSoftware = await response.Content.ReadFromJsonAsync<SoftwareObject>(CancellationToken);
        return createdSoftware;
    }

    public async Task UpdateSoftwareAsync(SoftwareObject software)
    {
        var response = await HttpClient.PutAsJsonAsync("api/softwares", software, CancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteSoftwareAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"api/softwares/{id}", CancellationToken);
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
