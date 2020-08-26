using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;
using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleSelectByQuery<TId, TModel, TSelectQuery> : SingleSelect<TId, TModel>, ISingleSelectByQuery<TId, TModel, TSelectQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
    {
        protected SingleSelectByQuery(TSelectQuery selectQuery)
        {
            SelectQuery = selectQuery;
        }

        public TSelectQuery SelectQuery { get; }

        protected override Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(SelectQuery.OperationalQueryQualifier.BuildQueryGroup(out QueryGroup queryGroup));

            if (!ReadOnlyLogs.Safely)
            {
                return null;
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

            return dbConnection.QueryAsync<TModel>(queryGroup, SelectQuery.OrderFieldQualifier.OrderFields, 1, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder)
                .ContinueWith(x => x.Result.FirstOrDefault());
        }
    }
}