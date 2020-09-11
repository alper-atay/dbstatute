using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Queries;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDeleteByProxy<TModel> : SingleDeleteBase<TModel>, ISingleDeleteByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleDeleteByProxy()
        {
            DeleteProxy = new DeleteProxy<TModel>();
        }

        public SingleDeleteByProxy(IDeleteProxy<TModel> deleteProxy)
        {
            DeleteProxy = deleteProxy ?? throw new ArgumentNullException(nameof(deleteProxy));
        }

        public IDeleteProxy<TModel> DeleteProxy { get; }

        IDeleteProxy ISingleDeleteByProxy.DeleteProxy => DeleteProxy;

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            TModel selectedModel = null;

            {
                if (DeleteProxy is IIdentifiableQuery identifiableQuery)
                {
                    selectedModel = await GetModelAsync(dbConnection, identifiableQuery.Id);
                }
            }

            {
                if (DeleteProxy is ISearchableQuery<TModel> searchableQuery)
                {
                    QueryGroup queryGroup = GetSearchableProxyResult(searchableQuery);

                    if (ReadOnlyLogs.Safely)
                    {
                        IEnumerable<Field> fields = DeleteProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;
                        IEnumerable<OrderField> orderFields = DeleteProxy is IOrderFieldableQuery<TModel> orderFieldableQuery ? GetOrderFieldableQueryResult(orderFieldableQuery) : null;

                        selectedModel = await GetModelAsync(dbConnection, queryGroup, fields, orderFields);
                    }
                }
            }

            {
                if (DeleteProxy is IModelableQuery<TModel> modelableQuery)
                {
                    TModel model = GetModelableQueryResult(modelableQuery);

                    if (!(model is null))
                    {
                        selectedModel = await GetModelAsync(dbConnection, model.Id);
                    }
                }
            }

            if (!(selectedModel is null))
            {
                int deletedCount = await dbConnection.DeleteAsync(selectedModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                if (deletedCount > 0)
                {
                    return selectedModel;
                }
            }

            return null;
        }
    }
}