using DbStatute.Fundamentals;
using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleInsert<TModel> : InsertBase<TModel>, ISingleInsert<TModel>
        where TModel : class, IModel, new()
    {
        private TModel _insertedModel;

        protected SingleInsert(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
        }

        public override int InsertedCount => _insertedModel is null ? 0 : 1;
        public TModel InsertedModel => (TModel)_insertedModel?.Clone();
        object ISingleInsert.InsertedModel => InsertedModel;
        public TModel RawModel { get; }
        object ISingleInsert.RawModel => RawModel;

        public async Task<TModel> InsertAsync(IDbConnection dbConnection)
        {
            _insertedModel = null;

            if (ReadOnlyLogs.Safely)
            {
                object insertedModelId = await InsertOperationAsync(dbConnection);
                _insertedModel = await dbConnection.QueryAsync<TModel>(insertedModelId, top: 1)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            StatuteResult = _insertedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return InsertedModel;
        }

        Task<object> ISingleInsert.InsertAsync(IDbConnection dbConnection)
        {
            return InsertAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected virtual async Task<object> InsertOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.InsertAsync(RawModel);
        }
    }
}