using Domain.Models;


namespace Domain.DTOs;

public class PostCreationDto
{
    public string Username { get; }
    public string Title { get; }
    public string Text { get; }
    
    public PostCreationDto( string username, string title, string text)
    {
        Username = username;
        Title = title;
        Text = text;
    }
}