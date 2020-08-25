using DbStatute.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelect<TId, TModel> : Select, IMultipleSelect<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        private readonly List<TModel> _selectedModels = new List<TModel>();

        public ICacheable Cacheable { get; set; }
        public int? MaxSelectCount { get; }
        public override int SelectedCount => _selectedModels.Count;
        public IEnumerable<TModel> SelectedModels => SelectedCount > 0 ? _selectedModels : null;

        public async IAsyncEnumerable<TModel> SelectAsSingular(IDbConnection dbConnection)
        {
            _selectedModels.Clear();

            await foreach (TModel selectedModel in SelectAsSingularOperationAsync(dbConnection))
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

        protected abstract IAsyncEnumerable<TModel> SelectAsSingularOperationAsync(IDbConnection dbConnection);

        protected abstract Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection);
    }
}