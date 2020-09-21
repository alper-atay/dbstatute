using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Queries;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleInsert<TModel> : MultipleInsertBase<TModel>, IMultipleInsert<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleInsert(IEnumerable<TModel> sourceModels)
        {
            SourceModels = sourceModels ?? throw new ArgumentNullException(nameof(sourceModels));
        }

        public IEnumerable<TModel> SourceModels { get; }

        IEnumerable<object> ISourceableModelsQuery.SourceModels => SourceModels;

        protected override async IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel sourceModel in SourceModels)
            {
                TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(sourceModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                yield return insertedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            int sourceModelCount = SourceModels.Count();

            if (sourceModelCount > 0)
            {
                int insertedCount = await dbConnection.InsertAllAsync(SourceModels, BatchSize, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (insertedCount != sourceModelCount)
                {
                    Logs.Warning($"{sourceModelCount} models selected and {insertedCount} models inserted");
                }

                if (insertedCount > 0)
                {
                    QueryField modelIdsInQuery = SourceModels.GetIdsInQuery();

                    IEnumerable<TModel> insertedModels = await dbConnection.QueryAsync<TModel>(modelIdsInQuery, null, null, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                    return insertedModels;
                }
            }

            return null;
        }
    }
}