using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Singles;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals.Singles
{
    public abstract class SingleSelectBase : SelectBase, ISingleSelectBase
    {
        private object _selectedModel;

        public override int? MaxSelectCount => 1;

        public override int SelectedCount => _selectedModel is null ? 0 : 1;

        public object SelectedModel => _selectedModel;

        public async Task<object> SelectAsync(IDbConnection dbConnection)
        {
            _selectedModel = await SelectOperationAsync(dbConnection);

            StatuteResult = _selectedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return SelectedModel;
        }

        protected abstract Task<object> SelectOperationAsync(IDbConnection dbConnection);
    }

    public abstract class SingleSelectBase<TModel> : SelectBase<TModel>, ISingleSelectBase<TModel>
        where TModel : class, IModel, new()
    {
        private TModel _selectedModel;

        public override int? MaxSelectCount => new int?(1);

        public override int SelectedCount => SelectedModel is null ? 0 : 1;

        public TModel SelectedModel => _selectedModel;

        object ISingleSelectBase.SelectedModel => SelectedModel;

        public async Task<TModel> SelectAsync(IDbConnection dbConnection)
        {
            _selectedModel = await SelectOperationAsync(dbConnection);

            StatuteResult = _selectedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return SelectedModel;
        }

        Task<object> ISingleSelectBase.SelectAsync(IDbConnection dbConnection)
        {
            return SelectAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected abstract Task<TModel> SelectOperationAsync(IDbConnection dbConnection);
    }
}