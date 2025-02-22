using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Departments;

namespace UIClient.Services;

public class CabinetsApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<CabinetObject>> GetCabinetsAsync() => await HttpClient.GetFromJsonAsync<List<CabinetObject>>("api/cabinets");

    public async Task<CabinetObject> AddCabinetAsync(CabinetObject cabinet)
    {
        var response = await HttpClient.PostAsJsonAsync("api/cabinets", cabinet, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdCabinet = await response.Content.ReadFromJsonAsync<CabinetObject>(CancellationToken);
        return createdCabinet;
    }
}
