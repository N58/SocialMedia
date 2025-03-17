using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Common;

namespace SocialMedia.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<Paged<T>> ToPagedAsync<T>(
        this IQueryable<T> query,
        int page,
        int size,
        CancellationToken cancellationToken = default) where T : class
    {
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new Paged<T>(items, totalCount, page, size);
    }

    public static IQueryable<T> ApplyOrdering<T>(
        this IQueryable<T> query,
        Expression<Func<T, object>>? orderBy = null,
        string? sortOrder = "asc")
    {
        if (orderBy == null) return query;

        var isDescending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);
        return isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
    }
}