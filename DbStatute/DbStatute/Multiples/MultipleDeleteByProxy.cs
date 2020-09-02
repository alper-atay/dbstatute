using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Proxies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleDeleteByProxy<TModel> : MultipleDeleteBase<TModel>, IMultipleDeleteByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleDeleteByProxy()
        {
            DeleteProxy = new DeleteProxy<TModel>();
        }

        public MultipleDeleteByProxy(IDeleteProxy<TModel> deleteProxy)
        {
            DeleteProxy = deleteProxy ?? throw new ArgumentNullException(nameof(deleteProxy));
        }

        public IDeleteProxy<TModel> DeleteProxy { get; }

        IDeleteProxy IMultipleDeleteByProxy.DeleteProxy => DeleteProxy;

        protected override IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection, bool allowNullReturnIfDeleted = false)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}