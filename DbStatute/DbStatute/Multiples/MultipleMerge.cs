using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Multiples;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleMerge<TModel> : MultipleMergeBase<TModel>, IMultipleMerge<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleMerge(IEnumerable<TModel> readyModels)
        {
            SourceModels = readyModels ?? throw new ArgumentNullException(nameof(readyModels));
        }

        public IEnumerable<TModel> SourceModels { get; }

        IEnumerable<object> ISourceableModelsQuery.SourceModels => SourceModels;

        protected override async IAsyncEnumerable<TModel> MergeAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel readyModel in SourceModels)
            {
                TModel mergedModel = await dbConnection.MergeAsync<TModel, TModel>(readyModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (mergedModel is null)
                {
                    continue;
                }

                yield return mergedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> MergeOperationAsync(IDbConnection dbConnection)
        {
            int readyModelCount = SourceModels.Count();

            if (readyModelCount > 0)
            {
                int mergedCount = await dbConnection.MergeAllAsync(SourceModels, BatchSize, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (mergedCount != readyModelCount)
                {
                    Logs.Warning($"{readyModelCount} models selected and {mergedCount} models merged");
                }

                if (mergedCount > 0)
                {
                    return SourceModels;
                }
            }

            return null;
        }
    }
}