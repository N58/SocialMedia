namespace SocialMedia.Domain.Common;

public class Paged<T>(ICollection<T> data, int totalCount, int page, int size)
{
    public int Count => Data.Count;
    public int TotalCount { get; } = totalCount;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (decimal)Size);

    public int Page { get; } = page;

    public int Size { get; } = size;

    public ICollection<T> Data { get; } = data;
}