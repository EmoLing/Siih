using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Departments;

namespace UIClient.Services;

public class CabinetsApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<CabinetObject>> GetCabinetsAsync() => await HttpClient.GetFromJsonAsync<List<CabinetObject>>("api/cabinets");

    public async Task<CabinetObject> GetCabinetAsync(int id) => await HttpClient.GetFromJsonAsync<CabinetObject>($"api/cabinets/{id}");

    public async Task<CabinetObject> AddCabinetAsync(CabinetObject cabinet)
    {
        var response = await HttpClient.PostAsJsonAsync("api/cabinets", cabinet, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdCabinet = await response.Content.ReadFromJsonAsync<CabinetObject>(CancellationToken);
        return createdCabinet;
    }

    public async Task UpdateCabinetAsync(CabinetObject cabinet)
    {
        var response = await HttpClient.PutAsJsonAsync("api/cabinets", cabinet, CancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteCabinetAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"api/cabinets/{id}", CancellationToken);
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
