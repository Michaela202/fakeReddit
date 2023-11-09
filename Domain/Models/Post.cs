namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public int OwnerId { get; set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }

  /*  public Post(User owner, string title, string text)
    {
        Owner = owner;
        Title = title;
        Text = text;
      
    }*/
    public Post(int ownerName, string userName, string title, string text)
    {
        OwnerId = ownerName;
        UserName = userName;
        Title = title;
        Text = text;
      
    }
    public Post(){}
}