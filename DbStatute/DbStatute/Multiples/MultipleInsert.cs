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
            foreach (TModel selectedModel in ReadyModels)
            {
                TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(selectedModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (insertedModel is null)
                {
                    continue;
                }

                yield return insertedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            int selectedCount = ReadyModels.Count();

            if (selectedCount > 0)
            {
                int insertedCount = await dbConnection.InsertAllAsync(ReadyModels, BatchSize, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (insertedCount != selectedCount)
                {
                    Logs.Warning($"{selectedCount} models selected and {insertedCount} models inserted");
                }

                if (insertedCount > 0)
                {
                    return ReadyModels;
                }
            }

            return null;
        }
    }
}