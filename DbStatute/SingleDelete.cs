using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleDelete<TId, TModel, TSingleSelect> : Delete
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
        where TSingleSelect : SingleSelect<TId, TModel>
    {
        protected SingleDelete(TSingleSelect singleSelect)
        {
            SingleSelect = singleSelect ?? throw new ArgumentNullException(nameof(singleSelect));
        }

        public TSingleSelect SingleSelect { get; }

        public async Task DeleteAsync(IDbConnection dbConnection)
        {
            DeletedCount = 0;

            if (ReadOnlyLogs.Safely)
            {
                DeletedCount = await DeleteOperationAsync(dbConnection);
            }

            if (DeletedCount == 0)
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