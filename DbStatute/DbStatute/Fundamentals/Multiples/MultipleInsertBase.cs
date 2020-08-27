using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Multiples;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Multiples
{
    public abstract class MultipleInsertBase<TModel> : InsertBase<TModel>, IMultipleInsertBase<TModel>
        where TModel : class, IModel, new()
    {
        private readonly List<TModel> _insertedModels = new List<TModel>();

        public int BatchSize { get; set; } = 10;
        public ICacheable Cacheable { get; set; }
        public override int InsertedCount => _insertedModels.Count;
        public IEnumerable<TModel> InsertedModels => InsertedCount > 0 ? _insertedModels : null;

        public async IAsyncEnumerable<TModel> InsertAsSingleAsync(IDbConnection dbConnection)
        {
            _insertedModels.Clear();

            await foreach (TModel insertedModel in InsertAsSingleOperationAsync(dbConnection))
            {
                if (insertedModel is null)
                {
                    continue;
                }

                _insertedModels.Add(insertedModel);

                yield return insertedModel;
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

        protected abstract IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection);

        protected abstract Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection);
    }
}