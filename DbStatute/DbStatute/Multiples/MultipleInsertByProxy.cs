using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Querying;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleInsertByProxy<TModel> : MultipleInsertBase<TModel>, IMultipleInsertByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleInsertByProxy()
        {
            InsertProxy = new InsertProxy<TModel>();
        }

        public MultipleInsertByProxy(IInsertProxy<TModel> insertProxy)
        {
            InsertProxy = insertProxy ?? throw new ArgumentNullException(nameof(insertProxy));
        }

        public IInsertProxy<TModel> InsertProxy { get; }

        IInsertProxy IMultipleInsertByProxy.InsertProxy => InsertProxy;

        protected override IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}