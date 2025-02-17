namespace SocialMedia.Domain.Common;

public class Paged<T>
{
    public Paged(ICollection<T> data, int totalCount, int size, int pageNumber)
    {
        Data = data;
        TotalCount = totalCount;
        Size = size;
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(TotalCount / (decimal)Size);
    }

    public int TotalCount { get; set; }
    public int Size { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; private set; }
    public ICollection<T> Data { get; set; }
}