using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleUpdate<TId, TModel, TUpdateQuery, TSingleSelect> : Update, ISingleUpdate<TId, TModel, TUpdateQuery, TSingleSelect>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
        where TSingleSelect : ISingleSelect<TId, TModel>
    {
        private TModel _updatedModel;

        protected SingleUpdate(TUpdateQuery updateQuery, TSingleSelect singleSelect)
        {
            UpdateQuery = updateQuery ?? throw new ArgumentNullException(nameof(updateQuery));
            SingleSelect = singleSelect ?? throw new ArgumentNullException(nameof(singleSelect));
        }

        public TSingleSelect SingleSelect { get; }
        public override int UpdatedCount => _updatedModel is null ? 0 : 1;
        public TModel UpdatedModel => (TModel)_updatedModel?.Clone();
        public TUpdateQuery UpdateQuery { get; }

        public async Task<TModel> UpdateAsync(IDbConnection dbConnection)
        {
            _updatedModel = await UpdateOperationAsync(dbConnection);

            StatuteResult = _updatedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return UpdatedModel;
        }

        protected virtual async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            TModel selectedModel = await SingleSelect.SelectAsync(dbConnection);
            Logs.AddRange(SingleSelect.ReadOnlyLogs);

            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            //TODO: Use field qualifier
            //Wait next RepoDb beta release
            //UpdateQuery.FieldQualifier
            int updateModelId = await dbConnection.UpdateAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            if (updateModelId > 0)
            {
                return await dbConnection.QueryAsync<TModel>(updateModelId, null, 1, Hints, null, commandTimeout: CommandTimeout, transaction: Transaction, trace: Trace, statementBuilder: StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}