using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
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

            ISelectQualifierGroup<TModel> selectQualifierGroup = selectQuery.SelectQualifierGroup;
            IOrderFieldQualifier<TModel> orderFieldQualifier = selectQuery.OrderFieldQualifier;

            Logs.AddRange(selectQualifierGroup.Build<TModel>(out QueryGroup queryGroup));

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