using System;
using System.Threading.Tasks;
using _Support;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace _2.API.Infra
{
    internal sealed class UnityOfWork : IUnityOfWork, IDisposable
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDomainValidation _domainValidation;
        private IDbContextTransaction _dbContextTransaction;

        public UnityOfWork(IApplicationDbContext dbContext, ILogger<UnityOfWork> logger, IDomainValidation domainValidation)
        {
            _dbContext = dbContext;
            _domainValidation = domainValidation;
        }

        public async Task SaveChanges()
        {
            await _dbContext.Instance.SaveChangesAsync();
        }

        public async Task SaveChangesIfDomainIsValid()
        {
            if (_domainValidation.IsValid)
            {
                await SaveChanges();
            }
        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _dbContext.Instance.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_dbContextTransaction == null)
                throw new InvalidOperationException("Transaction not started, are you sure you called BeginTransaction()");

            _dbContextTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (_dbContextTransaction == null)
                throw new InvalidOperationException("Transaction not started, are you sure you called BeginTransaction()");

            _dbContextTransaction.Rollback();
        }

        public void CommitTransactionIfDomainIsValid(Action executeAfterSaveIfValid = null)
        {
            if (_domainValidation.IsValid)
            {
                _dbContextTransaction.Commit();
                executeAfterSaveIfValid?.Invoke();
            }
            else
            {
                _dbContextTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbContextTransaction?.Dispose();
        }
    }
}