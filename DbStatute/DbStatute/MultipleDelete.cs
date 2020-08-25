using DbStatute.Interfaces;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleDelete<TId, TModel, TMultipleSelect> : Delete, IMultipleDelete<TId, TModel, TMultipleSelect>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TMultipleSelect : IMultipleSelect<TId, TModel>
    {
        private int _deletedCount;

        protected MultipleDelete(TMultipleSelect multipleSelect)
        {
            MultipleSelect = multipleSelect ?? throw new ArgumentNullException(nameof(multipleSelect));
        }

        public override int DeletedCount => _deletedCount;
        public TMultipleSelect MultipleSelect { get; }

        public async IAsyncEnumerable<TModel> DeleteAsSinglyAsync(IDbConnection dbConnection)
        {
            _deletedCount = 0;

            if (!ReadOnlyLogs.Safely)
            {
                yield break;
            }

            await foreach (TModel deletedModel in DeleteAsSinglyOperationAsync(dbConnection))
            {
                _deletedCount += 1;

                yield return deletedModel;
            }

            StatuteResult = _deletedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;
        }

        public async Task<int> DeleteAsync(IDbConnection dbConnection)
        {
            _deletedCount = 0;

            if (ReadOnlyLogs.Safely)
            {
                _deletedCount = await DeleteOperationAsync(dbConnection);
            }

            StatuteResult = _deletedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;

            return DeletedCount;
        }

        public async Task<int> DeleteByActingAsync(IDbConnection dbConnection, Action<TModel> action)
        {
            _deletedCount = 0;

            if (ReadOnlyLogs.Safely)
            {
                _deletedCount = await DeleteByActingOperationAsync(dbConnection, action);
            }

            StatuteResult = _deletedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;

            return DeletedCount;
        }

        protected virtual async IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            await foreach (TModel selectedModel in MultipleSelect.SelectAsSinglyAsync(dbConnection))
            {
                int deleteCount = await dbConnection.DeleteAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deleteCount == 0)
                {
                    continue;
                }

                yield return selectedModel;
            }
        }

        protected virtual async Task<int> DeleteByActingOperationAsync(IDbConnection dbConnection, Action<TModel> action)
        {
            await MultipleSelect.SelectByActingAsync(dbConnection, action);

            if (MultipleSelect.ReadOnlyLogs.Safely)
            {
                if (MultipleSelect.SelectedModels != null)
                {
                    return await dbConnection.DeleteAllAsync(MultipleSelect.SelectedModels, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                }
            }

            return 0;
        }

        protected virtual async Task<int> DeleteOperationAsync(IDbConnection dbConnection)
        {
            await MultipleSelect.SelectAsync(dbConnection);

            if (MultipleSelect.ReadOnlyLogs.Safely)
            {
                if (MultipleSelect.SelectedModels != null)
                {
                    return await dbConnection.DeleteAllAsync(MultipleSelect.SelectedModels, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                }
            }

            return 0;
        }
    }
}