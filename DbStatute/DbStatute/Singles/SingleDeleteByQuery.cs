using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDeleteByQuery<TModel, TDeleteQuery> : SingleDeleteBase<TModel>, ISingleDeleteByQuery<TModel, TDeleteQuery>
        where TModel : class, IModel, new()
        where TDeleteQuery : IDeleteQuery<TModel>
    {
        public SingleDeleteByQuery(TDeleteQuery deleteQuery)
        {
            DeleteQuery = deleteQuery ?? throw new ArgumentNullException(nameof(deleteQuery));
        }

        public TDeleteQuery DeleteQuery { get; }

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            ISelectQuery<TModel> selectQuery = DeleteQuery.SelectQuery;
            IOrderFieldQualifier<TModel> orderFieldQualifier = selectQuery.OrderFieldQualifier;
            ISelectQueryGroupBuilder<TModel> selectQueryGroupBuilder = selectQuery.SelectQueryGroupBuilder;

            Logs.AddRange(selectQueryGroupBuilder.Build(out QueryGroup queryGroup));
            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            IEnumerable<OrderField> orderFields = null;
            if (orderFieldQualifier.HasOrderField)
            {
                orderFields = orderFieldQualifier.OrderFields;
            }

            TModel deleteModel = await dbConnection.QueryAsync<TModel>(queryGroup, null, orderFields, 1, Hints, Cacheable?.Key, Cacheable.ItemExpiration ?? 180, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());

            if (deleteModel != null)
            {
                int deletedCount = await dbConnection.DeleteAsync(deleteModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return deleteModel;
        }
    }
}