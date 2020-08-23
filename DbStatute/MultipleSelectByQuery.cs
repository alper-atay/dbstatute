using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelectByQuery<TId, TModel, TSelectQuery> : MultipleSelect<TId, TModel>
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
        where TSelectQuery : ISelectQuery<TId, TModel>
    {
        protected MultipleSelectByQuery(TSelectQuery selectQuery)
        {
            SelectQuery = selectQuery ?? throw new ArgumentNullException(nameof(selectQuery));
        }

        public TSelectQuery SelectQuery { get; }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (ReadOnlyLogs.Safely)
            {
                IEnumerable<OrderField> orderFields = null;
                if (SelectQuery is ISortableQuery sortableQuery)
                {
                    if (sortableQuery.HasOrderField)
                    {
                        orderFields = sortableQuery.OrderFields;
                    }
                }

                return await dbConnection.QueryAsync<TModel>(SelectQuery.QueryGroup, orderFields);
            }

            return null;
        }
    }
}