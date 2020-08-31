using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Interfaces.Singles;
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
        where TDeleteProxy : IDeleteProxy<TModel>
    {
        public SingleDeleteByProxy(TDeleteProxy deleteProxy)
        {
            DeleteProxy = deleteProxy ?? throw new ArgumentNullException(nameof(deleteProxy));
        }

        public TDeleteProxy DeleteProxy { get; }

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            ISelectProxy<TModel> selectQuery = DeleteProxy.SelectProxy;
            IOrderFieldQualifier<TModel> orderFieldQualifier = selectQuery.OrderFieldQualifier;

            IEnumerable<OrderField> orderFields = null;
            if (orderFieldQualifier.HasOrderField)
            {
                orderFields = orderFieldQualifier.ReadOnlyOrderFields;
            }

            bool isQueryGroupBuilt = selectQuery.SelectQueryGroupBuilder.Build(out QueryGroup queryGroup);
            Logs.AddRange(selectQuery.SelectQueryGroupBuilder.ReadOnlyLogs);

            if (isQueryGroupBuilt)
            {
                TModel deleteModel = await dbConnection.QueryAsync<TModel>(queryGroup, null, orderFields, 1, Hints, Cacheable?.Key, Cacheable.ItemExpiration ?? 180, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());

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