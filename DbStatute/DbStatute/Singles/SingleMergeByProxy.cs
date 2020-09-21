using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
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
    public class SingleMergeByProxy<TModel> : SingleMergeBase<TModel>, ISingleMergeByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleMergeByProxy()
        {
            MergeProxy = new MergeProxy<TModel>();
        }

        public SingleMergeByProxy(IMergeProxy<TModel> mergeProxy)
        {
            MergeProxy = mergeProxy ?? throw new ArgumentNullException(nameof(mergeProxy));
        }

        public IMergeProxy<TModel> MergeProxy { get; }

        IMergeProxy ISingleMergeByProxy.MergeProxy => MergeProxy;

        protected override async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            {
                if (MergeProxy is ISearchableQuery<TModel> searchableQuery)
                {
                    QueryGroup queryGroup = GetSearchableProxyResult(searchableQuery);

                    IEnumerable<Field> fields = MergeProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;
                    IEnumerable<OrderField> orderFields = MergeProxy is IOrderFieldableQuery<TModel> orderFieldableQuery ? GetOrderFieldableQueryResult(orderFieldableQuery) : null;

                    if (!(queryGroup is null))
                    {
                        TModel searchModel = await GetModelAsync(dbConnection, queryGroup, fields, orderFields);

                        if (searchModel is null)
                        {
                            return null;
                        }

                        if (MergeProxy is IIdentifiableQuery identifiableQuery)
                        {
                            searchModel.Id = identifiableQuery.Id;

                            return (TModel)await dbConnection.MergeAsync(searchModel, fields, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                        }
                        else
                        {
                            TModel mergeModel = null;

                            if (MergeProxy is IModelableQuery<TModel> modelableQuery)
                            {
                                mergeModel = GetModelableQueryResult(modelableQuery);
                            }

                            if (MergeProxy is ISourceableQuery<TModel> sourceableModelQuery)
                            {
                                mergeModel = GetSourceableQueryResult(sourceableModelQuery);
                            }

                            if (mergeModel is null)
                            {
                                return null;
                            }

                            mergeModel.Id = searchModel.Id;

                            return (TModel)await dbConnection.MergeAsync(mergeModel, fields, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                        }
                    }
                }
            }

            {
                IEnumerable<Field> fields = MergeProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;

                TModel mergeModel = null;

                if (MergeProxy is ISourceableQuery<TModel> sourceableQuery)
                {
                    mergeModel = GetSourceableQueryResult(sourceableQuery);
                }

                if (MergeProxy is IModelableQuery<TModel> modelableQuery)
                {
                    mergeModel = GetModelableQueryResult(modelableQuery);
                }

                if (!(mergeModel is null))
                {
                    return (TModel)await dbConnection.MergeAsync(mergeModel, fields, null, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                }
            }

            return null;
        }
    }
}