using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Singles;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Singles
{
    public abstract class SingleInsertBase<TModel> : InsertBase<TModel>, ISingleInsertBase<TModel>
        where TModel : class, IModel, new()
    {
        private TModel _insertedModel;

        public override int InsertedCount => InsertedModel is null ? 0 : 1;
        public TModel InsertedModel => _insertedModel;
        object ISingleInsertBase.InsertedModel => InsertedModel;

        public async Task<TModel> InsertAsync(IDbConnection dbConnection)
        {
            _insertedModel = await InsertOperationAsync(dbConnection);

            StatuteResult = _insertedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return InsertedModel;
        }

        Task<object> ISingleInsertBase.InsertAsync(IDbConnection dbConnection)
        {
            return InsertAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected abstract Task<TModel> InsertOperationAsync(IDbConnection dbConnection);
    }
}