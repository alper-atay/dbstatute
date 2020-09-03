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
    public class MultipleUpdateByProxy<TModel> : MultipleUpdateBase<TModel>, IMultipleUpdateByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleUpdateByProxy()
        {
            UpdateProxy = new UpdateProxy<TModel>();
        }

        public MultipleUpdateByProxy(IUpdateProxy<TModel> updateProxy)
        {
            UpdateProxy = updateProxy ?? throw new ArgumentNullException(nameof(updateProxy));
        }

        public IUpdateProxy<TModel> UpdateProxy { get; }

        IUpdateProxy IMultipleUpdateByProxy.UpdateProxy => UpdateProxy;

        protected override IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {





            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}