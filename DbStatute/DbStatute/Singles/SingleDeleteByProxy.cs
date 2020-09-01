using DbStatute.Builders;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using DbStatute.Proxies;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDeleteByProxy<TModel, TDeleteProxy> : SingleDeleteBase<TModel>, ISingleDeleteByProxy<TModel, TDeleteProxy>
        where TModel : class, IModel, new()
        where TDeleteProxy : class, IDeleteProxy<TModel>
    {
        public SingleDeleteByProxy()
        {
            DeleteProxy = new DeleteProxy<TModel>() as TDeleteProxy;
        }

        public SingleDeleteByProxy(TDeleteProxy deleteProxy)
        {
            DeleteProxy = deleteProxy ?? throw new ArgumentNullException(nameof(deleteProxy));
        }

        public TDeleteProxy DeleteProxy { get; }

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            ISelectProxy<TModel> selectQuery = DeleteProxy.SelectProxy;
            ISelectQueryGroupBuilder<TModel> selectQueryGroupBuilder = selectQuery.SelectQueryGroupBuilder;
            IOrderFieldQualifier<TModel> orderFieldQualifier = selectQuery.OrderFieldQualifier;

            selectQueryGroupBuilder.Build(out QueryGroup queryGroup);
            Logs.AddRange(selectQuery.SelectQueryGroupBuilder.ReadOnlyLogs);

            IOrderFieldBuilder<TModel> orderFieldBuilder = new OrderFieldBuilder<TModel>(orderFieldQualifier);
            orderFieldBuilder.Build(out IEnumerable<OrderField> orderFields);
            Logs.AddRange(orderFieldBuilder.ReadOnlyLogs);

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