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
    public class SingleDeleteByQuery<TModel, TDeleteQuery> : SingleDeleteBase<TModel>, ISingleDeleteByQuery<TModel, TDeleteQuery>
        where TModel : class, IModel, new()
        where TDeleteQuery : IDeleteProxy<TModel>
    {
        public SingleDeleteByQuery(TDeleteQuery deleteQuery)
        {
            DeleteQuery = deleteQuery ?? throw new ArgumentNullException(nameof(deleteQuery));
        }

        public TDeleteQuery DeleteQuery { get; }

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            ISelectProxy<TModel> selectQuery = null;
            IOrderFieldQualifier<TModel> orderFieldQualifier = selectQuery.OrderFieldQualifier;

            IEnumerable<OrderField> orderFields = null;
            if (orderFieldQualifier.HasOrderField)
            {
                orderFields = orderFieldQualifier.ReadOnlyOrderFields;
            }

            // TODO
            TModel deleteModel = await dbConnection.QueryAsync<TModel>((QueryGroup)null/*queryGroup*/, null, orderFields, 1, Hints, Cacheable?.Key, Cacheable.ItemExpiration ?? 180, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());

            if (deleteModel != null)
            {
                int deletedCount = await dbConnection.DeleteAsync(deleteModel, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return deleteModel;
        }
    }
}