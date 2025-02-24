namespace SocialMedia.API.Responses;

public class PagedResponse<T>
{
    public required int TotalCount { get; init; }
    public required int Size { get; init; }
    public required int PageNumber { get; init; }
    public required int TotalPages { get; init; }
    public required ICollection<T> Data { get; init; }
}