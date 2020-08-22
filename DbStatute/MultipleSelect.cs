using DbStatute.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelect<TId, TModel> : Select
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
    {
        private readonly List<TModel> _selectedModels = new List<TModel>();

        public override int SelectedCount => _selectedModels.Count;
        public IEnumerable<TModel> SelectedModels => SelectedCount > 0 ? _selectedModels : null;

        public async Task<IEnumerable<TModel>> SelectAsync(IDbConnection dbConnection)
        {
            _selectedModels.Clear();

            if (Logs.Safely)
            {
                IEnumerable<TModel> selectedModels = await SelectOperationAsync(dbConnection);
                if (selectedModels != null)
                {
                    _selectedModels.AddRange(selectedModels);
                }
            }

            if (SelectedCount == 0)
            {
                OnFailed();
            }
            else
            {
                OnSucceed();
            }

            return SelectedModels;
        }

        protected abstract Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection);
    }
}