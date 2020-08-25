using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleInsert<TId, TModel> : Insert, IMultipleInsert<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly List<TModel> _insertedModels = new List<TModel>();

        protected MultipleInsert(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
        }

        public override int InsertedCount => _insertedModels.Count;
        public IEnumerable<TModel> InsertedModels => InsertedCount > 0 ? _insertedModels : null;
        public IEnumerable<TModel> RawModels { get; }

        public async Task<IEnumerable<TModel>> InsertAsync(IDbConnection dbConnection)
        {
            _insertedModels.Clear();

            if (ReadOnlyLogs.Safely)
            {
                await foreach (TModel insertedModel in InsertOperationAsync(dbConnection))
                {
                    if (insertedModel is null)
                    {
                        continue;
                    }

                    _insertedModels.Add(insertedModel);
                }
            }

            StatuteResult = InsertedModels is null ? StatuteResult.Failure : StatuteResult.Success;

            return InsertedModels;
        }

        protected virtual async IAsyncEnumerable<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            if (RawModels.Count() == 0)
            {
                yield return null;
            }

            foreach (TModel rawModel in RawModels)
            {
                TId insertedModelId = (TId)await dbConnection.InsertAsync(rawModel);
                TModel insertedModel = await dbConnection.QueryAsync<TModel>(insertedModelId, top: 1).ContinueWith(x => x.Result.FirstOrDefault());

                if (insertedModel is null)
                {
                    continue;
                }

                yield return insertedModel;
            }
        }
    }
}