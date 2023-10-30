using Domain.DTOs;
using Domain.Models;

namespace HTTPClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(
        string? userName, 
        int? userId, 
        string? titleContains,
        string? textContains
    );
    Task<IEnumerable<Post>> GetAsyncByName(
        string? userName
    );
    Task<PostBasicDto> GetByIdAsync(int id);
}