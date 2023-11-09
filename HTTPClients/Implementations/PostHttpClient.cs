using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.DTOs;
using Domain.Models;
using HTTPClients.ClientInterfaces;

namespace HTTPClients.Implementations;

public class PostHttpClient: IPostService
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/post",dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Post>> GetAsync(int? userId, string? textContains, string? titleContains, string? userName)
    {
        string query = ConstructQuery( userId,textContains,titleContains,userName);
        HttpResponseMessage responseMessage = await client.GetAsync("/Post"+query);
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        })!;
        return posts;
    }


   
    public async Task<IEnumerable<Post>> GetAsyncByName(string? userNameContains = null)
    {
        string uri = "/post";
        if (!string.IsNullOrEmpty(userNameContains))
        {
            uri += $"?username={userNameContains}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<Post> posts = JsonSerializer.Deserialize<IEnumerable<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    private static string ConstructQuery( int? userId,  string? textContains,string? titleContains,string? userName)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"?username={userName}";
        }

        if (userId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"userid={userId}";
        }
        
        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={titleContains}";
        }
//isn't working properly ...delete later (maybe)
        if (!string.IsNullOrEmpty(textContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"textcontains={textContains}";
        }
        return query;
    }
    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/post/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        PostBasicDto todo = JsonSerializer.Deserialize<PostBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return todo;
    }
}