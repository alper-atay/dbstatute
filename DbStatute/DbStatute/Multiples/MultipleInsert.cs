using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
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
        public MultipleInsert(IEnumerable<TModel> readyModels)
        {
            ReadyModels = readyModels ?? throw new ArgumentNullException(nameof(readyModels));
        }

        public IEnumerable<TModel> ReadyModels { get; }

        IEnumerable<object> IReadyModels.ReadyModels => ReadyModels;

        protected override async IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel readyModel in ReadyModels)
            {
                TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(readyModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                yield return insertedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            int readyModelCount = ReadyModels.Count();

            if (readyModelCount > 0)
            {
                int insertedCount = await dbConnection.InsertAllAsync(ReadyModels, BatchSize, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (insertedCount != readyModelCount)
                {
                    Logs.Warning($"{readyModelCount} models selected and {insertedCount} models inserted");
                }

                if (insertedCount > 0)
                {
                    QueryField modelIdsInQuery = ReadyModels.GetIdsInQuery();

                    IEnumerable<TModel> insertedModels = await dbConnection.QueryAsync<TModel>(modelIdsInQuery, null, null, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                    return insertedModels;
                }
            }

            return null;
        }
    }
}