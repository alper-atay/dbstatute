using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Multiples;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleUpdate<TModel> : MultipleUpdateBase<TModel>, IMultipleUpdate<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleUpdate(IEnumerable<TModel> readyModels)
        {
            SourceModels = readyModels ?? throw new ArgumentNullException(nameof(readyModels));
        }

        public IEnumerable<TModel> SourceModels { get; }

        IEnumerable<object> ISourceableModelsQuery.SourceModels => SourceModels;

        protected override async IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel readyModel in SourceModels)
            {
                int updatedCount = await dbConnection.UpdateAsync(readyModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (updatedCount > 0)
                {
                    yield return readyModel;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection)
        {
            int updatedCount = await dbConnection.UpdateAllAsync(SourceModels, BatchSize, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            if (updatedCount > 0)
            {
                return SourceModels;
            }

            return null;
        }
    }
}