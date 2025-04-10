using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Interfaces;
using SocialMedia.Domain.Common;

namespace SocialMedia.Infrastructure.Repositories;

internal abstract class BaseRepository<TBaseEntity>(DbContext dbContext)
    : IBaseRepository<TBaseEntity> where TBaseEntity : BaseEntity
{
    public async Task<TBaseEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TBaseEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(TBaseEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(TBaseEntity entity)
    {
        entity.UpdatedDate = DateTime.Now;
        dbContext.Set<TBaseEntity>().Update(entity);
    }

    public async Task DeleteAsync(TBaseEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TBaseEntity>().Where(x => x.Id == entity.Id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ICollection<TBaseEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TBaseEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }
}