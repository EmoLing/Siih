using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Equipment;

namespace UIClient.Services;

public class ComplexesHardwareApiServices(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<ComplexHardwareObject>> GetComplexesHardwareAsync() => await HttpClient.GetFromJsonAsync<List<ComplexHardwareObject>>("api/complexeshardware");

    public async Task<ComplexHardwareObject> AddComplexHardwareAsync(ComplexHardwareObject complexHardware)
    {
        var response = await HttpClient.PostAsJsonAsync("api/complexeshardware", complexHardware, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdComplexHardware = await response.Content.ReadFromJsonAsync<ComplexHardwareObject>(CancellationToken);
        return createdComplexHardware;
    }
}
