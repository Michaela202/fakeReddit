namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string?  Username { get; }
    public int? UserId { get; }
    public string? TitleContains { get; }

    public SearchPostParametersDto(int? userId, string titleContains,string? username)
    {
        Username = username;
        UserId = userId;
        TitleContains = titleContains;
    }
}