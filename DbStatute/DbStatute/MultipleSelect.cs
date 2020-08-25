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

        public override int SelectedCount => _selectedModels.Count;
        public IEnumerable<TModel> SelectedModels => SelectedCount > 0 ? _selectedModels : null;

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

        protected abstract Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection);
    }
}