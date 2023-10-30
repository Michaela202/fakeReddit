using Domain.DTOs;
using Domain.Models;

namespace HTTPClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    Task<IEnumerable<User>> AsyncGetUsers(string? usernameContains = null);
    
}