using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByQuery<TModel, TInsertQuery> : SingleInsertBase<TModel>, ISingleInsertByQuery<TModel, TInsertQuery>
        where TModel : class, IModel, new()
        where TInsertQuery : IInsertQuery<TModel>
    {
        public SingleInsertByQuery(TInsertQuery insertQuery)
        {
            InsertQuery = insertQuery ?? throw new ArgumentNullException(nameof(TInsertQuery));
        }

        public TInsertQuery InsertQuery { get; }

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {

            IMergeQueryGroupBuilder<TModel> mergeQueryGroupBuilder = InsertQuery.MergeQueryGroupBuilder;

            Logs.AddRange(mergeQueryGroupBuilder.Build(out QueryGroup queryGroup));

            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            IFieldBuilder<TModel> fieldBuilder = mergeQueryGroupBuilder.FieldBuilder;
            fieldBuilder.Build(out IEnumerable<Field> fields);


            return await dbConnection.InsertAsync<TModel, TModel>((TModel)insertModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
        }
    }
}