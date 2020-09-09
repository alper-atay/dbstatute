using Basiclog;
using DbStatute.Extensions;
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
    public class SingleUpdateByProxy<TModel> : SingleUpdateBase<TModel>, ISingleUpdateByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleUpdateByProxy()
        {
            UpdateProxy = new UpdateProxy<TModel>();
        }

        public SingleUpdateByProxy(IUpdateProxy<TModel> updateProxy)
        {
            UpdateProxy = updateProxy ?? throw new ArgumentNullException(nameof(updateProxy));
        }

        public IUpdateProxy<TModel> UpdateProxy { get; }

        IUpdateProxy ISingleUpdateByProxy.UpdateProxy => UpdateProxy;

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            object updateId = default;
            int updatedCount = 0;

            {
                if (UpdateProxy is ISearchableQuery<TModel> searchableQuery)
                {
                    QueryGroup queryGroup = GetSearchableProxyResult(searchableQuery);

                    IEnumerable<Field> fields = UpdateProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;
                    IEnumerable<OrderField> orderFields = UpdateProxy is IOrderFieldableQuery<TModel> orderFieldableQuery ? GetOrderFieldableQueryResult(orderFieldableQuery) : null;

                    if (!(queryGroup is null))
                    {
                        TModel searchModel = await GetModelAsync(dbConnection, queryGroup, fields, orderFields);

                        if (searchModel is null)
                        {
                            return null;
                        }

                        if (UpdateProxy is IIdentifiableQuery identifiableQuery)
                        {
                            updateId = identifiableQuery.Id;

                            updatedCount = await dbConnection.UpdateAsync(searchModel, updateId, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                        }
                        else
                        {
                            TModel updateModel = null;

                            if (UpdateProxy is IModelableQuery<TModel> modelableQuery)
                            {
                                updateModel = GetModelableQueryResult(modelableQuery);
                            }

                            if (UpdateProxy is ISourceableQuery<TModel> sourceableModelQuery)
                            {
                                updateModel = GetSourceableModelQueryResult(sourceableModelQuery);
                            }

                            if (updateModel is null)
                            {
                                return null;
                            }

                            updateId = updateModel.Id = searchModel.Id;

                            updatedCount = await dbConnection.UpdateAsync(updateModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                        }
                    }
                }
            }

            {
                if (UpdateProxy is ISourceableQuery<TModel> sourceableModelQuery)
                {
                    TModel sourceModel = GetSourceableModelQueryResult(sourceableModelQuery);

                    if (sourceModel is null)
                    {
                        return null;
                    }

                    IEnumerable<Field> fields = UpdateProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;

                    if (UpdateProxy is IIdentifiableQuery identifiableQuery)
                    {
                        updateId = identifiableQuery.Id;

                        updatedCount = await dbConnection.UpdateAsync(sourceModel, updateId, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                    }
                    else
                    {
                        updateId = sourceModel.Id;

                        updatedCount = await dbConnection.UpdateAsync(sourceModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                    }
                }
            }

            {
                if (UpdateProxy is IModelableQuery<TModel> modelableQuery)
                {
                    IReadOnlyLogbook modelableQueryLogs = modelableQuery.ModelQuery.Build(out TModel model);

                    Logs.AddRange(modelableQueryLogs);

                    if (!modelableQueryLogs.Safely && model is null)
                    {
                        return null;
                    }

                    IEnumerable<Field> fields = UpdateProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;

                    if (UpdateProxy is IIdentifiableQuery identifiableQuery)
                    {
                        updateId = identifiableQuery.Id;

                        updatedCount = await dbConnection.UpdateAsync(model, updateId, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                    }
                    else
                    {
                        updateId = model.Id;

                        updatedCount = await dbConnection.UpdateAsync(model, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                    }
                }
            }

            if (updatedCount > 0)
            {
                return await GetModelAsync(dbConnection, updateId);
            }

            return null;
        }
    }
}