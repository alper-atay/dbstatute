using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleDelete<TId, TModel, TMultipleSelect> : Delete
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
        where TMultipleSelect : MultipleSelect<TId, TModel>
    {
        protected MultipleDelete(TMultipleSelect multipleSelect)
        {
            MultipleSelect = multipleSelect ?? throw new ArgumentNullException(nameof(multipleSelect));
        }

        public TMultipleSelect MultipleSelect { get; }

        public async Task DeleteAsync(IDbConnection dbConnection)
        {
            if (Logs.Safely)
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
            await MultipleSelect.SelectAsync(dbConnection);

            if (MultipleSelect.ReadOnlyLogs.Safely)
            {
                if (MultipleSelect.SelectedModels != null)
                {
                    return await dbConnection.DeleteAllAsync(MultipleSelect.SelectedModels);
                }
            }

            return 0;
        }
    }
}