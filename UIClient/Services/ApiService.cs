using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Shared.DTOs.Departments;
using Shared.DTOs.Equipment;
using Shared.DTOs.Users;

namespace UIClient.Services;

public abstract class ApiService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly CancellationToken _cancellationToken = new();

    protected HttpClient HttpClient => _httpClient;

    protected CancellationToken CancellationToken => _cancellationToken;
}
