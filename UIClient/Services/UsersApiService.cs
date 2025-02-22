using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs.Users;

namespace UIClient.Services;

public class UsersApiService(HttpClient httpClient) : ApiService(httpClient)
{
    public async Task<List<UserObject>> GetUsersAsync() => await HttpClient.GetFromJsonAsync<List<UserObject>>("api/users");

    public async Task<UserObject> GetUserAsync(int id) => await HttpClient.GetFromJsonAsync<UserObject>($"api/users/{id}");

    public async Task<UserObject> AddUserAsync(UserObject user)
    {
        var response = await HttpClient.PostAsJsonAsync("api/users", user, CancellationToken);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<UserObject>(CancellationToken);
        return createdUser;
    }

    public async Task UpdateUserAsync(UserObject user)
    {
        var response = await HttpClient.PutAsJsonAsync("api/users", user, CancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var response = await HttpClient.DeleteAsync($"api/users/{userId}", CancellationToken);
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
