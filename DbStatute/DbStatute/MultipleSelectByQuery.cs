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
    public abstract class MultipleSelectByQuery<TModel, TSelectQuery> : MultipleSelect<TModel>, IMultipleSelectByQuery<TModel, TSelectQuery>

        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
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

            IEnumerable<OrderField> orderFields = SelectQuery.OrderFieldQualifier.OrderFields;

            ICache cache = null;
            int cacheItemExpiration = 180;
            string cacheKey = null;

            if (Cacheable != null)
            {
                cache = Cacheable.Cache;
                cacheItemExpiration = Cacheable.ItemExpiration ?? 180;
                cacheKey = Cacheable.Key;
            }

            Logs.AddRange(SelectQuery.OperationalQueryQualifier.GetQueryGroup(out QueryGroup queryGroup));

            return await dbConnection.QueryAsync<TModel>(queryGroup, orderFields, MaxSelectCount, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder);
        }
    }
}