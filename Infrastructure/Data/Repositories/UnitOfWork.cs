namespace Infrastructure.Data.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext) => _dbContext = dbContext;
    
    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}