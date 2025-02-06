namespace SocialMedia.Application.Interfaces;

public interface IBaseRepository<TBaseEntity>
{
    Task<TBaseEntity?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<TBaseEntity>> GetAllAsync();

    Task AddAsync(TBaseEntity entity);

    Task UpdateAsync(TBaseEntity entity);

    Task DeleteAsync(TBaseEntity entity);
    Task SaveChangesAsync();
}