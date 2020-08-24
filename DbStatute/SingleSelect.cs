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
        private TModel _selectedModel;

        public override int SelectedCount => SelectedModel is null ? 0 : 1;
        public TModel SelectedModel => (TModel)_selectedModel?.Clone();

        public async Task<TModel> SelectAsync(IDbConnection dbConnection)
        {
            _selectedModel = null;

            if (Logs.Safely)
            {
                _selectedModel = await SelectOperationAsync(dbConnection);
            }

            StatuteResult = _selectedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return SelectedModel;
        }

        protected abstract Task<TModel> SelectOperationAsync(IDbConnection dbConnection);
    }
}