using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Singles;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Singles
{
    public abstract class SingleDeleteBase<TModel> : DeleteBase<TModel>, ISingleDeleteBase<TModel>
        where TModel : class, IModel, new()
    {
        private TModel _deletedModel;

        public override int DeletedCount => DeletedModel is null ? 0 : 1;
        public TModel DeletedModel => _deletedModel;
        object ISingleDeleteBase.DeletedModel => DeletedModel;

        public async Task<TModel> DeleteAsync(IDbConnection dbConnection)
        {
            _deletedModel = await DeleteOperationAsync(dbConnection);

            StatuteResult = _deletedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return DeletedModel;
        }

        Task<object> ISingleDeleteBase.DeleteAsync(IDbConnection dbConnection)
        {
            return DeleteAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected abstract Task<TModel> DeleteOperationAsync(IDbConnection dbConnection);
    }
}