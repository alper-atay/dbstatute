using DbStatute.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelect<TModel> : Select, IMultipleSelect<TModel>

        where TModel : class, IModel, new()
    {
        private readonly List<TModel> _selectedModels = new List<TModel>();

        public int? MaxSelectCount { get; }
        public Type ModelType => throw new NotImplementedException();
        public override int SelectedCount => _selectedModels.Count;
        public IEnumerable<TModel> SelectedModels => SelectedCount > 0 ? _selectedModels : null;
        IEnumerable<object> IMultipleSelect.SelectedModels => SelectedModels;

        public async IAsyncEnumerable<TModel> SelectAsSinglyAsync(IDbConnection dbConnection)
        {
            _selectedModels.Clear();

            await foreach (TModel selectedModel in SelectAsSignlyOperationAsync(dbConnection))
            {
                if (selectedModel is null)
                {
                    continue;
                }

                yield return selectedModel;

                _selectedModels.Add(selectedModel);
            }

            StatuteResult = SelectedModels is null ? StatuteResult.Failure : StatuteResult.Success;
        }

        IAsyncEnumerable<object> IMultipleSelect.SelectAsSinglyAsync(IDbConnection dbConnection)
        {
            return SelectAsSignlyOperationAsync(dbConnection);
        }

        public async Task<IEnumerable<TModel>> SelectAsync(IDbConnection dbConnection)
        {
            _selectedModels.Clear();

            IEnumerable<TModel> selectedModels = await SelectOperationAsync(dbConnection);
            if (selectedModels != null)
            {
                _selectedModels.AddRange(selectedModels);
            }

            StatuteResult = SelectedModels is null ? StatuteResult.Failure : StatuteResult.Success;

            return SelectedModels;
        }

        Task<IEnumerable<object>> IMultipleSelect.SelectAsync(IDbConnection dbConnection)
        {
            return SelectAsync(dbConnection).ContinueWith(x => x.Result.Cast<object>());
        }

        public async Task<IEnumerable<TModel>> SelectByActingAsync(IDbConnection dbConnection, Action<TModel> action)
        {
            _selectedModels.Clear();

            IEnumerable<TModel> selectedModels = await SelectOperationAsync(dbConnection);
            if (selectedModels != null)
            {
                _selectedModels.AddRange(selectedModels);

                foreach (TModel selectedModel in selectedModels)
                {
                    action.Invoke(selectedModel);
                }
            }

            StatuteResult = SelectedModels is null ? StatuteResult.Failure : StatuteResult.Success;

            return SelectedModels;
        }

        public Task<IEnumerable<object>> SelectByActingAsync(IDbConnection dbConnection, Action<object> action)
        {
            return SelectByActingAsync(dbConnection, action).ContinueWith(x => x.Result.Cast<object>());
        }

        protected abstract IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection);

        protected abstract Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection);
    }
}