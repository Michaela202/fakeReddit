using Domain.DTOs;
using Domain.Models;

namespace HTTPClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(
        
        int? userId, 
        string? textContains,
        string? titleContains,
    string? userName
    );
    Task<IEnumerable<Post>> GetAsyncByName(
        string? userName
    );
    Task<PostBasicDto> GetByIdAsync(int id);
}