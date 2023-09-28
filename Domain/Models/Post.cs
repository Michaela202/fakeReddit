namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public string Text { get; set; }

    public Post(User owner, string title, string text)
    {
        Owner = owner;
        Title = title;
        Text = text;
    }
}