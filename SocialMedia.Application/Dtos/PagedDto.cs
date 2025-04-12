namespace SocialMedia.Application.Dtos;

public class PagedDto<T>
{
    public required int Count { get; init; }
    public required int TotalCount { get; init; }
    public required int TotalPages { get; init; }
    public required int Page { get; init; }
    public required int Size { get; init; }
    public required ICollection<T> Data { get; init; }
}