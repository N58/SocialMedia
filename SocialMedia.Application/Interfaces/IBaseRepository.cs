namespace SocialMedia.Application.Interfaces;

public interface IBaseRepository<TBaseEntity>
{
    Task<TBaseEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<TBaseEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TBaseEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TBaseEntity entity);

    Task DeleteAsync(TBaseEntity entity, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}