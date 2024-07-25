#nullable disable
using Microsoft.EntityFrameworkCore.Storage;
using Template.Domain.Interfaces.Services.UnitOfWork;
using Template.Infrastructure.DatabaseContext;
using System.Diagnostics;
using System.Transactions;

namespace Template.Infrastructure.Services.UnitOfWork
{
    public class UnitOfWork(TemplateDatabaseContext dbContext) : IUnitOfWork, IDisposable
    {
        private readonly TemplateDatabaseContext _dbContext = dbContext;

        private readonly Guid _instanceId = Guid.NewGuid();
        public Guid InstanceId => _instanceId;

        private bool _autoSave = true;
        private IDbContextTransaction _dbTransaction;

        public IDbContextTransaction CurrentTransaction => _dbTransaction;
        public event Action<IDbContextTransaction> OnBeginTransaction;

        public bool AutoSave => _autoSave;

        public void DisableAutoSave()
            => _autoSave = false;

        public void EnableAutoSave()
            => _autoSave = true;

        public static TransactionScope CreateScopedTransactionStatic(TransactionScopeOption option = TransactionScopeOption.Required)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            if (!Debugger.IsAttached)
            {
                transactionOptions.Timeout = TimeSpan.FromSeconds(30);
            }
            else
            {
                transactionOptions.Timeout = TransactionManager.MaximumTimeout;
            }

            var tran = new TransactionScope(option, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
            return tran;
        }

        public TransactionScope CreateScopedTransaction(TransactionScopeOption option = TransactionScopeOption.Required)
            => CreateScopedTransactionStatic(option);

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellation = default)
        {
            if (_dbTransaction == null)
            {
                _dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellation);
                OnBeginTransaction?.Invoke(_dbTransaction);
            }

            return _dbTransaction;
        }

        public void Dispose()
        {
            _dbTransaction?.Dispose();
            _dbContext?.Dispose();
        }
    }
}
