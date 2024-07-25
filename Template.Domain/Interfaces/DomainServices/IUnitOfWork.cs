using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace Template.Domain.Interfaces.Services.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    event Action<IDbContextTransaction> OnBeginTransaction;

    void DisableAutoSave();
    void EnableAutoSave();
    TransactionScope CreateScopedTransaction(TransactionScopeOption option = TransactionScopeOption.Required);
    bool AutoSave { get; }
    IDbContextTransaction CurrentTransaction { get; }
    Guid InstanceId { get; }

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellation = default);
}