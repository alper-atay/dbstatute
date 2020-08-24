using DbStatute.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleSelect<TId, TModel> : Select
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public override int SelectedCount => SelectedModel is null ? 0 : 1;
        public TModel SelectedModel { get; private set; }

        public async Task<TModel> SelectAsync(IDbConnection dbConnection)
        {
            SelectedModel = null;

            if (Logs.Safely)
            {
                SelectedModel = await SelectOperationAsync(dbConnection);
            }

            if (SelectedModel is null)
            {
                OnFailed();
            }
            else
            {
                OnSucceed();
            }

            return SelectedModel;
        }

        protected abstract Task<TModel> SelectOperationAsync(IDbConnection dbConnection);
    }
}