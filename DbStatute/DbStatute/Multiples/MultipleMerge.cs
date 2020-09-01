using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleMerge<TModel> : MultipleMergeBase<TModel>, IMultipleMerge<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleMerge(IEnumerable<TModel> readyModels)
        {
            ReadyModels = readyModels ?? throw new ArgumentNullException(nameof(readyModels));
        }

        public IEnumerable<TModel> ReadyModels { get; }

        IEnumerable<object> IReadyModels.ReadyModels => ReadyModels;

        protected override async IAsyncEnumerable<TModel> MergeAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel readyModel in ReadyModels)
            {
                yield return await dbConnection.MergeAsync<TModel, TModel>(readyModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }
        }

        protected override async Task<IEnumerable<TModel>> MergeOperationAsync(IDbConnection dbConnection)
        {
            int mergedCount = await dbConnection.MergeAllAsync<TModel>(ReadyModels, BatchSize, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            return mergedCount > 0 ? ReadyModels : null;
        }
    }
}