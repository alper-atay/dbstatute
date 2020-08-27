using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Multiples;
using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public class MultipleInsertByRawModels<TModel> : MultipleInsertBase<TModel>, IMultipleInsertBase<TModel>
        where TModel : class, IModel, new()
    {
        protected MultipleInsertByRawModels(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
        }

        public IEnumerable<TModel> RawModels { get; }

        protected override async IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            if (!ReadOnlyLogs.Safely)
            {
                yield break;
            }

            ICache cache = null;
            int cacheItemExpiration = 180;
            string cacheKey = null;

            if (Cacheable != null)
            {
                cache = Cacheable.Cache;
                cacheItemExpiration = Cacheable.ItemExpiration ?? 180;
                cacheKey = Cacheable.Key;
            }

            foreach (TModel rawModel in RawModels)
            {
                object insertedModelId = await dbConnection.InsertAsync(rawModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                TModel insertedModel = await dbConnection.QueryAsync<TModel>(insertedModelId, null, 1, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());

                if (insertedModel is null)
                {
                    continue;
                }

                yield return insertedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            // TODO
            // Collect inserted models with Trace ?
            int insertedModelCount = await dbConnection.InsertAllAsync(RawModels, BatchSize, Hints, CommandTimeout, Transaction, null /*Trace*/, StatementBuilder);

            if (insertedModelCount > 0)
            {
                return Enumerable.Empty<TModel>();
            }

            return null;
        }
    }
}