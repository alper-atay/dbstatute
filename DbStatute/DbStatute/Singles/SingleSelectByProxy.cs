using DbStatute.Builders;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleSelectByProxy<TModel, TSelectProxy> : SingleSelectBase<TModel>, ISingleSelectByProxy<TModel, TSelectProxy>
        where TModel : class, IModel, new()
        where TSelectProxy : class, ISelectProxy<TModel>
    {
        public SingleSelectByProxy()
        {
            SelectProxy = new SelectProxy<TModel>() as TSelectProxy;
        }

        public SingleSelectByProxy(TSelectProxy selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public TSelectProxy SelectProxy { get; }

        protected override async Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            ISelectQueryGroupBuilder<TModel> selectQueryGroupBuilder = SelectProxy.SelectQueryGroupBuilder;

            if (selectQueryGroupBuilder.Build(out QueryGroup queryGroup))
            {
                Logs.AddRange(selectQueryGroupBuilder.ReadOnlyLogs);

                IFieldQualifier<TModel> selectedFieldQualifier = SelectProxy.SelectedFieldQualifier;
                IOrderFieldQualifier<TModel> orderFieldQualifier = SelectProxy.OrderFieldQualifier;

                FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(selectedFieldQualifier);
                fieldBuilder.Build(out IEnumerable<Field> fields);
                Logs.AddRange(fieldBuilder.ReadOnlyLogs);

                OrderFieldBuilder<TModel> orderFieldBuilder = new OrderFieldBuilder<TModel>(orderFieldQualifier);
                orderFieldBuilder.Build(out IEnumerable<OrderField> orderFields);
                Logs.AddRange(orderFieldBuilder.ReadOnlyLogs);

                return await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}