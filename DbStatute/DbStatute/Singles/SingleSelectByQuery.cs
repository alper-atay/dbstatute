using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using RepoDb.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleSelectByQuery<TModel, TSelectQuery> : SingleSelect<TModel>, ISingleSelectByQuery<TModel, TSelectQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
    {
        protected SingleSelectByQuery(TSelectQuery selectQuery)
        {
            SelectQuery = selectQuery;
        }

        public TSelectQuery SelectQuery { get; }

        protected override Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            IOperationalQueryQualifier<TModel> operationalQueryQualifier = SelectQuery.OperationalQueryQualifier;
            Logs.AddRange(operationalQueryQualifier.GetQueryGroup(out QueryGroup queryGroup));

            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            IOrderFieldQualifier<TModel> orderFieldQualifier = SelectQuery.OrderFieldQualifier;
            IEnumerable<OrderField> orderFields = null;

            if (orderFieldQualifier.HasOrderField)
            {
                orderFields = orderFieldQualifier.OrderFields;
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

            return dbConnection.QueryAsync<TModel>(queryGroup, orderFields, 1, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());
        }
    }
}