using Notification.Application.Abstractions.Transaction;
using Notification.Persistence.Context;

namespace Notification.Persistence.Repositories;
 

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly NotificationDbContext _context;

    public UnitOfWork(NotificationDbContext context)
    {
        _context = context;
    }

 
    public async Task SaveChangesAsync()
    {
      await  _context.SaveChangesAsync();
    }
}
