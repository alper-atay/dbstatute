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
    public class MultipleUpdateByProxy<TModel> : MultipleUpdateBase<TModel>, IMultipleUpdateByProxy<TModel>
            where TModel : class, IModel, new()
    {
        public MultipleUpdateByProxy(IEnumerable<TModel> rawModels, IUpdateProxy<TModel> updateProxy)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            UpdateProxy = updateProxy ?? throw new ArgumentNullException(nameof(updateProxy));
        }

        public IEnumerable<TModel> RawModels { get; }

        IEnumerable<object> IRawModels.RawModels => RawModels;

        public IUpdateProxy<TModel> UpdateProxy { get; }

        IUpdateProxy IMultipleUpdateByProxy.UpdateProxy { get; }

        protected override async IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel rawModel in RawModels)
            {
                yield return rawModel;
            }


        }

        protected override Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}