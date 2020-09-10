using Basiclog;
using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Proxies;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleSelectByProxy<TModel> : MultipleSelectBase<TModel>, IMultipleSelectByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleSelectByProxy()
        {
            SelectProxy = new SelectProxy<TModel>();
        }

        public MultipleSelectByProxy(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy IMultipleSelectByProxy.SelectProxy => SelectProxy;

        protected override async IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection)
        {
            IEnumerable<TModel> selectedModels = await SelectAsync(dbConnection);

            if (IsSucceed)
            {
                foreach (TModel selectedModel in selectedModels)
                {
                    yield return selectedModel;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            {
                if (SelectProxy is IIdentifiablesQuery identifiablesQuery)
                {
                    IEnumerable<Field> fields = SelectProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;

                    return await dbConnection.QueryAsync<TModel>(identifiablesQuery.Ids, fields, null, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);
                }
            }

            {
                if (SelectProxy is ISearchableQuery<TModel> searchableQuery)
                {
                    IReadOnlyLogbook searchableQueryLogs = searchableQuery.Build(out QueryGroup queryGroup);

                    Logs.AddRange(searchableQueryLogs);

                    if (searchableQueryLogs.Safely)
                    {
                        IEnumerable<Field> fields = SelectProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;
                        IEnumerable<OrderField> orderFields = SelectProxy is IOrderFieldableQuery<TModel> orderFieldableQuery ? GetOrderFieldableQueryResult(orderFieldableQuery) : null;

                        return await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);
                    }
                }
            }

            return null;
        }
    }
}