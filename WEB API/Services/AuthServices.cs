using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;

namespace WEB_API.Services;

public class AuthServices: IAuthService
{
    private readonly IUserDao UserDao;

    public AuthServices(IUserDao userDao)
    {
        UserDao = userDao;
    }
    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await UserDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
    

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        //users.Add(user);
        
        return Task.CompletedTask;
    }
}