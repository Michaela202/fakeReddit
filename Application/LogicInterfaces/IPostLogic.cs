using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<PostBasicDto> GetByIdAsync(int id);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto);
}