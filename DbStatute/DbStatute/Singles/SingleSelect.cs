using DbStatute.Fundamentals;
using DbStatute.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleSelect<TModel> : SelectBase<TModel>, ISingleSelect<TModel>

        where TModel : class, IModel, new()
    {
        private TModel _selectedModel;

        public override int SelectedCount => SelectedModel is null ? 0 : 1;
        public TModel SelectedModel => (TModel)_selectedModel?.Clone();
        object ISingleSelect.SelectedModel => SelectedModel;

        public async Task<TModel> SelectAsync(IDbConnection dbConnection)
        {
            _selectedModel = null;

            if (ReadOnlyLogs.Safely)
            {
                _selectedModel = await SelectOperationAsync(dbConnection);
            }

            StatuteResult = _selectedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return SelectedModel;
        }

        Task<object> ISingleSelect.SelectAsync(IDbConnection dbConnection)
        {
            return SelectAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected abstract Task<TModel> SelectOperationAsync(IDbConnection dbConnection);
    }
}