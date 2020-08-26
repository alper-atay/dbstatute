using DbStatute.Interfaces;
using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleInsert<TModel> : Insert, IMultipleInsert<TModel>

        where TModel : class, IModel, new()
    {
        private readonly List<TModel> _insertedModels = new List<TModel>();

        protected MultipleInsert(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
        }

        public int BatchSize { get; set; } = 10;
        public ICacheable Cacheable { get; set; }
        public override int InsertedCount => _insertedModels.Count;
        public IEnumerable<TModel> InsertedModels => InsertedCount > 0 ? _insertedModels : null;
        public IEnumerable<TModel> RawModels { get; }

        public async IAsyncEnumerable<TModel> InsertAsSingleAsync(IDbConnection dbConnection)
        {
            _insertedModels.Clear();

            await foreach (TModel insertedModel in InsertAsSingleOperationAsync(dbConnection))
            {
                if (insertedModel is null)
                {
                    continue;
                }

                yield return insertedModel;

                _insertedModels.Add(insertedModel);
            }

            StatuteResult = InsertedModels is null ? StatuteResult.Failure : StatuteResult.Success;
        }

        public async Task<IEnumerable<TModel>> InsertAsync(IDbConnection dbConnection)
        {
            _insertedModels.Clear();

            IEnumerable<TModel> insertedModels = await InsertOperationAsync(dbConnection);
            if (insertedModels != null)
            {
                _insertedModels.AddRange(insertedModels);
            }

            StatuteResult = InsertedModels is null ? StatuteResult.Failure : StatuteResult.Success;

            return InsertedModels;
        }

        protected virtual async IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
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
                TModel insertedModel = await dbConnection.QueryAsync<TModel>(insertedModelId, null, 1, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());

                if (insertedModel is null)
                {
                    continue;
                }

                yield return insertedModel;
            }
        }

        protected virtual async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
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