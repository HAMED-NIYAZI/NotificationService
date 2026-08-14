namespace Notification.Application.Abstractions.Transaction;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
