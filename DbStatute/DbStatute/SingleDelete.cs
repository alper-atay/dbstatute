using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleDelete<TId, TModel, TSingleSelect> : Delete, ISingleDelete<TId, TModel, TSingleSelect>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSingleSelect : SingleSelect<TId, TModel>
    {
        private int _deletedCount;

        protected SingleDelete(TSingleSelect singleSelect)
        {
            SingleSelect = singleSelect ?? throw new ArgumentNullException(nameof(singleSelect));
        }

        public override int DeletedCount => _deletedCount;
        public TSingleSelect SingleSelect { get; }

        public async Task DeleteAsync(IDbConnection dbConnection)
        {
            _deletedCount = 0;

            if (ReadOnlyLogs.Safely)
            {
                _deletedCount = await DeleteOperationAsync(dbConnection);
            }

            if (_deletedCount == 0)
            {
                OnFailed();
            }
            else
            {
                OnSucceed();
            }
        }

        protected virtual async Task<int> DeleteOperationAsync(IDbConnection dbConnection)
        {
            await SingleSelect.SelectAsync(dbConnection);

            if (SingleSelect.SelectedModel != null)
            {
                return await dbConnection.DeleteAsync(SingleSelect.SelectedModel);
            }

            return 0;
        }
    }
}