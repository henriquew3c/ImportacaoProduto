using System;
using System.Threading.Tasks;

namespace _2.API.Infra
{
    public interface IUnityOfWork
    {
        Task SaveChanges();

        Task SaveChangesIfDomainIsValid();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void CommitTransactionIfDomainIsValid(Action executeAfterSaveIfValid = null);
    }
}