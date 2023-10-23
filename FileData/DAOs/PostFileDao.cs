using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao: IPostDao
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();
        if (!string.IsNullOrEmpty(searchPostParametersDto.Username))
        {
            result = context.Posts.Where(post =>
                post.Owner.UserName.Equals(searchPostParametersDto.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchPostParametersDto.UserId != null)
        {
            result = result.Where(p => p.Owner.Id == searchPostParametersDto.UserId);
        }

        if (!string.IsNullOrEmpty(searchPostParametersDto.TitleContains))
        {
            result = result.Where((p) =>
                p.Title.Contains(searchPostParametersDto.TitleContains, StringComparison.OrdinalIgnoreCase));
        }
        return Task.FromResult(result);
    }

    public Task<Post?> GetByTitleAsync(string title)
    {
        Post? exists =
            context.Posts.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exists);
    }

    public Task<Post?> GetByIdAsync(int postId)
    {
        Post? exists = context.Posts.FirstOrDefault(p => p.Id == postId);
        return Task.FromResult<Post?>(exists);
    }
}