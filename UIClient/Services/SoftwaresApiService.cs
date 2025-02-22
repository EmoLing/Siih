using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Equipment;

namespace UIClient.Services;

public class SoftwaresApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<SoftwareObject>> GetSoftwaresAsync() => await HttpClient.GetFromJsonAsync<List<SoftwareObject>>("api/softwares");

    public async Task<SoftwareObject> AddSoftwareAsync(SoftwareObject software)
    {
        var response = await HttpClient.PostAsJsonAsync("api/softwares", software, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdSoftware = await response.Content.ReadFromJsonAsync<SoftwareObject>(CancellationToken);
        return createdSoftware;
    }
}
