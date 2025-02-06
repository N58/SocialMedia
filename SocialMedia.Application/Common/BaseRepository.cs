using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Common;

namespace SocialMedia.Application.Common;

public abstract class BaseRepository<TBaseEntity>(DbContext dbContext) where TBaseEntity : BaseEntity
{
    public async Task<TBaseEntity?> GetByIdAsync(Guid id)
    {
        return await dbContext.Set<TBaseEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<TBaseEntity>> GetAllAsync()
    {
        return await dbContext.Set<TBaseEntity>().ToListAsync();
    }
    
    public async Task AddAsync(TBaseEntity entity)
    {
        await dbContext.AddAsync(entity);
    }

    public async Task UpdateAsync(TBaseEntity entity)
    {
        dbContext.Set<TBaseEntity>().Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TBaseEntity entity)
    {
        await dbContext.Set<TBaseEntity>().Where(x => x.Id == entity.Id).ExecuteDeleteAsync();
    }
}