using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleMergeByProxy<TModel, TMergeProxy> : MultipleMergeBase<TModel>, IMultipleMergeByProxy<TModel, TMergeProxy>
        where TModel : class, IModel, new()
        where TMergeProxy : class, IMergeProxy<TModel>
    {
        public MultipleMergeByProxy(TMergeProxy mergeProxy)
        {
            MergeProxy = mergeProxy ?? throw new ArgumentNullException(nameof(mergeProxy));
        }

        public TMergeProxy MergeProxy { get; }

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