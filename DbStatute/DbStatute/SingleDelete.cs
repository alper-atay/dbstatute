using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleDelete<TModel, TSingleSelect> : Delete<TModel>, ISingleDelete<TModel, TSingleSelect>

        where TModel : class, IModel, new()
        where TSingleSelect : ISingleSelect<TModel>
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

            StatuteResult = _deletedCount > 0 ? StatuteResult.Success : StatuteResult.Failure;
        }

        protected virtual async Task<int> DeleteOperationAsync(IDbConnection dbConnection)
        {
            await SingleSelect.SelectAsync(dbConnection);

            if (SingleSelect.SelectedModel != null)
            {
                return await dbConnection.DeleteAsync(SingleSelect.SelectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return 0;
        }
    }
}