using Basiclog;
using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Queries;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleDeleteByProxy<TModel> : MultipleDeleteBase<TModel>, IMultipleDeleteByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public IDeleteProxy<TModel> DeleteProxy { get; }

        public MultipleDeleteByProxy(IDeleteProxy<TModel> deleteProxy)
        {
            DeleteProxy = deleteProxy ?? throw new ArgumentNullException(nameof(deleteProxy));
        }

        IDeleteProxy IMultipleDeleteByProxy.DeleteProxy => DeleteProxy;

        protected override IAsyncEnumerable<TModel> DeleteAsSinglyOperationAsync(IDbConnection dbConnection)
        {


            throw new NotImplementedException();
        }

        protected override async Task<IEnumerable<TModel>> DeleteOperationAsync(IDbConnection dbConnection)
        {
            IEnumerable<TModel> selectedModels = null;

            {
                if (DeleteProxy is IIdentifiablesQuery identifiablesQuery)
                {
                    IEnumerable<Field> fields = DeleteProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;

                    selectedModels = await dbConnection.QueryAsync<TModel>(identifiablesQuery.Ids, fields, null, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);
                }
            }

            {
                if (DeleteProxy is ISearchableQuery<TModel> searchableQuery)
                {
                    IReadOnlyLogbook searchableQueryLogs = searchableQuery.Build(out QueryGroup queryGroup);

                    Logs.AddRange(searchableQueryLogs);

                    if (searchableQueryLogs.Safely)
                    {
                        IEnumerable<Field> fields = DeleteProxy is IFieldableQuery<TModel> fieldableQuery ? GetFieldableQueryResult(fieldableQuery) : null;
                        IEnumerable<OrderField> orderFields = DeleteProxy is IOrderFieldableQuery<TModel> orderFieldableQuery ? GetOrderFieldableQueryResult(orderFieldableQuery) : null;

                        selectedModels = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, null, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);
                    }
                }
            }

            if (!(selectedModels is null) && selectedModels.Count() > 0)
            {
                int deletedCount = await dbConnection.DeleteAllAsync(selectedModels, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }


            return null;
        }
    }
}