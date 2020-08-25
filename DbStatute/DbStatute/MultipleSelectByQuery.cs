using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelectByQuery<TId, TModel, TSelectQuery> : MultipleSelect<TId, TModel>, IMultipleSelectByQuery<TId, TModel, TSelectQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
    {
        protected MultipleSelectByQuery(TSelectQuery selectQuery)
        {
            SelectQuery = selectQuery ?? throw new ArgumentNullException(nameof(selectQuery));
        }

        public TSelectQuery SelectQuery { get; }

        // Select all queried rows
        // Can we pick one by one in DB?
        protected override async IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection)
        {
            if (!ReadOnlyLogs.Safely)
            {
                yield break;
            }

            IEnumerable<TModel> selectedModels = await SelectOperationAsync(dbConnection);

            foreach (TModel selectedModel in selectedModels)
            {
                yield return selectedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            IEnumerable<OrderField> orderFields = null;
            if (SelectQuery is ISortableQuery sortableQuery)
            {
                if (sortableQuery.HasOrderField)
                {
                    orderFields = sortableQuery.OrderFields;
                }
            }

            ICache cache = null;
            int cacheItemExpiration = 180;
            string cacheKey = null;

            if (Cacheable != null)
            {
                cache = Cacheable.Cache;
                cacheItemExpiration = Cacheable.ItemExpiration ?? 180;
                cacheKey = Cacheable.Key;
            }

            return await dbConnection.QueryAsync<TModel>(SelectQuery.QueryGroup, orderFields, MaxSelectCount, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder);
        }
    }
}