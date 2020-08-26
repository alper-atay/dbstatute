using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleUpdate<TId, TModel, TUpdateQuery, TMultipleSelect> : Update, IMultipleUpdate<TId, TModel, TUpdateQuery, TMultipleSelect>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
        where TMultipleSelect : IMultipleSelect<TId, TModel>
    {
        private int _updatedCount;

        protected MultipleUpdate(TMultipleSelect multipleSelect, TUpdateQuery updateQuery)
        {
            MultipleSelect = multipleSelect;
        }

        public int BatchSize { get; set; } = 10;
        public TMultipleSelect MultipleSelect { get; }
        public override int UpdatedCount => _updatedCount;
        public TUpdateQuery UpdateQuery { get; }

        public async IAsyncEnumerable<TModel> UpdateAsSinglyAsync(IDbConnection dbConnection)
        {
            _updatedCount = 0;

            if (!ReadOnlyLogs.Safely)
            {
                yield break;
            }

            await foreach (TModel updatedModel in UpdateAsSinglyOperationAsync(dbConnection))
            {
                _updatedCount += 1;

                yield return updatedModel;
            }

            StatuteResult = _updatedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;
        }

        public async Task<int> UpdateAsync(IDbConnection dbConnection)
        {
            _updatedCount = 0;

            if (ReadOnlyLogs.Safely)
            {
                _updatedCount = await UpdateOperationAsync(dbConnection);
            }

            StatuteResult = _updatedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;

            return UpdatedCount;
        }

        public async Task<int> UpdateByActingAsync(IDbConnection dbConnection, Action<TModel> action)
        {
            _updatedCount = 0;

            if (ReadOnlyLogs.Safely)
            {
                _updatedCount = await UpdateByActingOperationAsync(dbConnection, action);
            }

            StatuteResult = _updatedCount == 0 ? StatuteResult.Failure : StatuteResult.Success;

            return UpdatedCount;
        }

        protected virtual async IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            await foreach (TModel selectedModel in MultipleSelect.SelectAsSinglyAsync(dbConnection))
            {
                int updateCount = await dbConnection.UpdateAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (updateCount == 0)
                {
                    continue;
                }

                yield return selectedModel;
            }
        }

        protected async Task<int> UpdateByActingOperationAsync(IDbConnection dbConnection, Action<TModel> action)
        {
            await MultipleSelect.SelectByActingAsync(dbConnection, action);

            if (MultipleSelect.ReadOnlyLogs.Safely)
            {
                if (MultipleSelect.SelectedModels != null)
                {
                    // Can we get updated row with Trace???
                    await dbConnection.UpdateAllAsync(MultipleSelect.SelectedModels, BatchSize, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                }
            }

            return 0;
        }

        protected async Task<int> UpdateOperationAsync(IDbConnection dbConnection)
        {
            await MultipleSelect.SelectAsync(dbConnection);

            if (MultipleSelect.ReadOnlyLogs.Safely)
            {
                if (MultipleSelect.SelectedModels != null)
                {
                    // Can we get updated row with Trace???
                    await dbConnection.UpdateAllAsync(MultipleSelect.SelectedModels, BatchSize, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                }
            }

            return 0;
        }
    }
}