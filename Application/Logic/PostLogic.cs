using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic: IPostLogic
{
    private readonly IUserDao UserDao;
    private readonly IPostDao PostDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.PostDao = postDao;
        this.UserDao = userDao;
    }

    public  async Task<Post> CreateAsync(PostCreationDto postCreationDto)
    {
        User? user = await UserDao.GetByUsernameAsync(postCreationDto.Username);
        if (user == null)
        {
            throw new Exception($"User with username {postCreationDto.Username} was not found.");
        }

        Post post = new Post(user.Id, user.UserName,postCreationDto.Title, postCreationDto.Text);
        ValidatePost(postCreationDto);
        
        Post created = await PostDao.CreateAsync(post);
        return created;

    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        return PostDao.GetAsync(searchPostParametersDto);
    }
    public  async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await PostDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new PostBasicDto(post.Id, post.Text, post.Title, post.UserName);
    }
    

    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
    }
}