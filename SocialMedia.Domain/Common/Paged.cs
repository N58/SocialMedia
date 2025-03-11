namespace SocialMedia.Domain.Common;

public class Paged<T>(ICollection<T> data, int totalCount, int size, int pageNumber)
{
    public int TotalCount { get; } = totalCount;
    public int Size { get; } = size;
    public int PageNumber { get; } = pageNumber;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (decimal)Size);
    public ICollection<T> Data { get; } = data;
}

