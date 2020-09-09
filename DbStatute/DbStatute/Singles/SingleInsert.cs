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
    public class SingleInsert<TModel> : SingleInsertBase<TModel>, ISingleInsert<TModel>
        where TModel : class, IModel, new()
    {
        public SingleInsert(TModel sourceModel)
        {
            SourceModel = sourceModel ?? throw new ArgumentNullException(nameof(sourceModel));
        }

        public TModel SourceModel { get; }

        object ISourceableQuery.SourceModel => SourceModel;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            return await dbConnection.InsertAsync<TModel, TModel>(SourceModel, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
        }
    }
}