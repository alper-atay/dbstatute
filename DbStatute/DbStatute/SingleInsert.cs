using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleInsert<TId, TModel> : Insert, ISingleInsert<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private TModel _insertedModel;

        protected SingleInsert(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
        }

        public override int InsertedCount => _insertedModel is null ? 0 : 1;
        public TModel InsertedModel => (TModel)_insertedModel?.Clone();
        public TModel RawModel { get; }

        public async Task<TModel> InsertAsync(IDbConnection dbConnection)
        {
            _insertedModel = null;

            if (ReadOnlyLogs.Safely)
            {
                TId insertedModelId = await InsertOperationAsync(dbConnection);
                _insertedModel = await dbConnection.QueryAsync<TModel>(insertedModelId, top: 1)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            StatuteResult = _insertedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return InsertedModel;
        }

        protected virtual async Task<TId> InsertOperationAsync(IDbConnection dbConnection)
        {
            return (TId)await dbConnection.InsertAsync(RawModel);
        }
    }
}