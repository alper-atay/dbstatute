using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            ISelectProxy<TModel> selectQuery = DeleteProxy.SelectProxy;

            IWhereQuery<TModel> whereQuery = selectQuery.WhereQuery;
            IOrderFieldQualifier<TModel> orderFieldQualifier = selectQuery.OrderFieldQualifier;

            Logs.AddRange(whereQuery.Build<TModel>(Conjunction.And, out QueryGroup queryGroup));

            bool orderFieldsBuilt = orderFieldQualifier.Build<TModel>(out IEnumerable<OrderField> orderFields);

            if (!orderFieldsBuilt)
            {
                orderFields = null;
            }

            if (queryGroup != null)
            {
                TModel deleteModel = await dbConnection.QueryAsync<TModel>(queryGroup, null, orderFields, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());

                if (deleteModel != null)
                {
                    int deletedCount = await dbConnection.DeleteAsync(deleteModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
                }

                return deleteModel;
            }

            return null;
        }
    }
}