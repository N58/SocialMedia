namespace SocialMedia.API.Responses;

public class PagedResponse<T>
{
    public required int TotalCount { get; set; }
    public required int Size { get; set; }
    public required int PageNumber { get; set; }
    public required int TotalPages { get; set; }
    public required ICollection<T> Data { get; set; }
}