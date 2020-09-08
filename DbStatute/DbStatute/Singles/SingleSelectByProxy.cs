using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Proxies;
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
    public class SingleSelectByProxy<TModel> : SingleSelectBase<TModel>, ISingleSelectByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public SingleSelectByProxy()
        {
            SelectProxy = new SelectProxy<TModel>();
        }

        public SingleSelectByProxy(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy ISingleSelectByProxy.SelectProxy => SelectProxy;

        protected override async Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(SelectProxy.SearchQuery.Build(Conjunction.And, out QueryGroup queryGroup));

            if (Logs.Safely)
            {
                IEnumerable<Field> fields = null;
                IEnumerable<OrderField> orderFields = null;

                if (SelectProxy is IFieldableQuery<TModel> fieldableQuery)
                {
                    bool fieldsBuilt = fieldableQuery.FieldQuery.Fields.Build(out fields);

                    if (!fieldsBuilt)
                    {
                        fields = null;
                    }
                }

                if (SelectProxy is IOrderFieldableQuery<TModel> orderFieldableQuery)
                {
                    bool orderFieldsBuilt = orderFieldableQuery.OrderFieldQuery.OrderFields.Build(out orderFields);

                    if (!orderFieldsBuilt)
                    {
                        orderFields = null;
                    }
                }

                return await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}