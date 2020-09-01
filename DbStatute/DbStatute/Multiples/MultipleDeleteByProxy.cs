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
    public class MultipleDeleteByProxy<TModel, TDeleteProxy> : MultipleDeleteBase<TModel>, IMultipleDeleteByProxy<TModel, TDeleteProxy>
        where TModel : class, IModel, new()
        where TDeleteProxy : class, IDeleteProxy<TModel>
    {
        public MultipleDeleteByProxy()
        {
            DeleteProxy = new DeleteProxy<TModel>() as TDeleteProxy;
        }

        public TDeleteProxy DeleteProxy { get; }

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