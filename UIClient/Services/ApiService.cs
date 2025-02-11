﻿using DB.Models.Departments;
using DB.Models.Users;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

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

    public async Task<User> AddUserAsync(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users", user, _cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdUser = await response.Content.ReadFromJsonAsync<User>(_cancellationToken);
        return createdUser;
    }

    public async Task<User> UpdateUserAsync(User originalUser, User editedUser)
    {
        try
        {
            var patchDoc = CreatePatch(originalUser, editedUser);

            var json = JsonConvert.SerializeObject(patchDoc.Operations);
            var content = new StringContent(json, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync($"api/users/{originalUser.Id}", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<User>(_cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Ошибка при обновлении пользователя: {ex.Message}");
            return null;
        }
    }

    public JsonPatchDocument<User> CreatePatch(User originalUser, User updatedUser)
    {
        var patchDoc = new JsonPatchDocument<User>();

        if (originalUser.Name != updatedUser.Name)
            patchDoc.Replace(u => u.Name, updatedUser.Name);

        if (originalUser.JobTitle != updatedUser.JobTitle)
            patchDoc.Replace(u => u.JobTitle, updatedUser.JobTitle);

        if (originalUser.Cabinet != updatedUser.Cabinet)
            patchDoc.Replace(u => u.Cabinet, updatedUser.Cabinet);

        return patchDoc;
    }

    public async Task<List<Department>> GetDepartmentsAsync() => await _httpClient.GetFromJsonAsync<List<Department>>("api/departments");
    public async Task<List<Cabinet>> GetCabinetsAsync() => await _httpClient.GetFromJsonAsync<List<Cabinet>>("api/cabinets");
    public async Task<List<JobTitle>> GetJobTitlesAsync() => await _httpClient.GetFromJsonAsync<List<JobTitle>>("api/jobtitles");
}
