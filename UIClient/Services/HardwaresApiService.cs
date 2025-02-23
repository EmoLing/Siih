using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Equipment;

namespace UIClient.Services;

public class HardwaresApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<HardwareObject>> GetHardwaresAsync() => await HttpClient.GetFromJsonAsync<List<HardwareObject>>("api/hardwares");

    public async Task<HardwareObject> GetHardwareAsync(int id) => await HttpClient.GetFromJsonAsync<HardwareObject>($"api/hardwares/{id}");

    public async Task<HardwareObject> AddHardwareAsync(HardwareObject hardware)
    {
        var response = await HttpClient.PostAsJsonAsync("api/hardwares", hardware, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdHardware = await response.Content.ReadFromJsonAsync<HardwareObject>(CancellationToken);
        return createdHardware;
    }

    public async Task UpdateHardwareAsync(HardwareObject hardware)
    {
        var response = await HttpClient.PutAsJsonAsync("api/hardwares", hardware, CancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteHardwareAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"api/hardwares/{id}", CancellationToken);
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
