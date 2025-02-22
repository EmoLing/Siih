using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.DTOs.Users;

namespace UIClient.Services;

public class JobTitlesApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<JobTitleObject>> GetJobTitlesAsync() => await HttpClient.GetFromJsonAsync<List<JobTitleObject>>("api/jobtitles");

    public async Task<JobTitleObject> AddJobTitleAsync(JobTitleObject jobTitle)
    {
        var response = await HttpClient.PostAsJsonAsync("api/jobtitles", jobTitle, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdJobTitle = await response.Content.ReadFromJsonAsync<JobTitleObject>(CancellationToken);
        return createdJobTitle;
    }

    public async Task UpdateJobTitleAsync(JobTitleObject user)
    {
        var response = await HttpClient.PutAsJsonAsync("api/jobtitles", user, CancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteJobTitleAsync(int jobTitleId)
    {
        var response = await HttpClient.DeleteAsync($"api/jobtitles/{jobTitleId}", CancellationToken);
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
