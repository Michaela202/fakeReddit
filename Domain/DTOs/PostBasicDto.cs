namespace Domain.DTOs;

public class PostBasicDto
{
    public int Id { get; set; }
    public string OwnerName { get; set; }
    
    public string Title { get; set; }
    public string Text { get; set; }
    
    public PostBasicDto(int id, string text,string title,string ownerName)
    {
        Id = id;
        OwnerName = ownerName;
        Title = title;
        Text = text;
    }
}