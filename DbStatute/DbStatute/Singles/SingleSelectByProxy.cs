using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleSelectByProxy<TModel> : SingleSelectBase<TModel>, ISingleSelectByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleSelectByProxy()
        {
            SelectProxy = new SelectProxy<TModel>();
        }

        public SingleSelectByProxy(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy ISingleSelectByProxy.SelectProxy => SelectProxy;

        protected override async Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            {
                if (SelectProxy is IIdentifiableQuery identifiableQuery)
                {
                    return await GetModelAsync(dbConnection, identifiableQuery.Id);
                }
            }

            {
                if (SelectProxy is ISearchableQuery<TModel> searchableQuery)
                {
                    QueryGroup queryGroup = GetSearchableProxyResult(searchableQuery);

                    if (ReadOnlyLogs.Safely)
                    {
                        IEnumerable<Field> fields = SelectProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;
                        IEnumerable<OrderField> orderFields = SelectProxy is IOrderFieldableQuery<TModel> orderFieldableQuery ? GetOrderFieldableQueryResult(orderFieldableQuery) : null;

                        return await GetModelAsync(dbConnection, queryGroup, fields, orderFields);
                    }
                }
            }

            return null;
        }
    }
}