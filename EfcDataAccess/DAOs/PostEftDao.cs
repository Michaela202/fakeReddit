using System.Text.Json;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEftDao: IPostDao
{
    private readonly PostContext context;

    public PostEftDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public  async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
    {
        IQueryable<Post> query = context.Posts.Include(todo => todo.Owner).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(todo =>
                todo.Owner.UserName.ToLower().Equals(searchParams.Username.ToLower()));
        }
    
        if (searchParams.UserId != null)
        {
            query = query.Where(t => t.Owner.Id == searchParams.UserId);
        }
    
       
    
        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParams.TitleContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        Post? found = await context.Posts
            .Include(p => p.Owner)
            .SingleOrDefaultAsync(p => p.Id == id);
        return found;
    }
}