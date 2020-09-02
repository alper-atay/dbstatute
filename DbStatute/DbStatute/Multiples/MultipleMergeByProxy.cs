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
    public class MultipleMergeByProxy<TModel> : MultipleMergeBase<TModel>, IMultipleMergeByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleMergeByProxy()
        {
            MergeProxy = new MergeProxy<TModel>();
        }

        public MultipleMergeByProxy(IMergeProxy<TModel> mergeProxy)
        {
            MergeProxy = mergeProxy ?? throw new ArgumentNullException(nameof(mergeProxy));
        }

        public IMergeProxy<TModel> MergeProxy { get; }

        IMergeProxy IMultipleMergeByProxy.MergeProxy => MergeProxy;

        protected override IAsyncEnumerable<TModel> MergeAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<TModel>> MergeOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}