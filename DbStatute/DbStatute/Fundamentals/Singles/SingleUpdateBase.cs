using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Singles;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Singles
{
    public abstract class SingleUpdateBase<TModel> : UpdateBase<TModel>, ISingleUpdateBase<TModel>
        where TModel : class, IModel, new()
    {
        private TModel _updatedModel;

        public override int UpdatedCount => UpdatedModel is null ? 0 : 1;
        public TModel UpdatedModel => _updatedModel;
        object ISingleUpdateBase.UpdatedModel => UpdatedModel;

        public async Task<TModel> UpdateAsync(IDbConnection dbConnection)
        {
            _updatedModel = await UpdateOperationAsync(dbConnection);

            StatuteResult = _updatedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return UpdatedModel;
        }

        Task<object> ISingleUpdateBase.UpdateAsync(IDbConnection dbConnection)
        {
            return UpdateAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected abstract Task<TModel> UpdateOperationAsync(IDbConnection dbConnection);
    }
}