using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdate<TModel> : SingleUpdateBase<TModel>, ISingleUpdate<TModel>
        where TModel : class, IModel, new()
    {
        public SingleUpdate(TModel sourceModel)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public TModel SourceModel { get; }

        object ISourceableModelQuery.SourceModel => SourceModel;

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            int updatedCount = await dbConnection.UpdateAsync(SourceModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

            if (updatedCount > 0)
            {
                SingleSelectById<TModel> singleSelectById = new SingleSelectById<TModel>(SourceModel.Id);

                Logs.AddRange(singleSelectById.ReadOnlyLogs);

                if (singleSelectById.IsSucceed)
                {
                    return singleSelectById.SelectedModel;
                }
            }

            return null;
        }
    }
}