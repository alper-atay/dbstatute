using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Interfaces.Singles;
using DbStatute.Querying;
using DbStatute.Querying.Builders;
using RepoDb;
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
            ISelectQueryGroupBuilder<TModel> selectQueryGroupBuilder = SelectProxy.SelectQueryGroupBuilder;

            if (selectQueryGroupBuilder.Build(out QueryGroup queryGroup))
            {
                Logs.AddRange(selectQueryGroupBuilder.ReadOnlyLogs);

                IFieldQualifier<TModel> selectedFieldQualifier = SelectProxy.SelectedFieldQualifier;
                IOrderFieldQualifier<TModel> orderFieldQualifier = SelectProxy.OrderFieldQualifier;

                IEnumerable<Field> fields = null;
                FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(selectedFieldQualifier);
                fieldBuilder.Build(out fields);
                Logs.AddRange(fieldBuilder.ReadOnlyLogs);

                IEnumerable<OrderField> orderFields = null;
                OrderFieldBuilder<TModel> orderFieldBuilder = new OrderFieldBuilder<TModel>(orderFieldQualifier);
                orderFieldBuilder.Build(out orderFields);
                Logs.AddRange(orderFieldBuilder.ReadOnlyLogs);

                if (!ReadOnlyLogs.Safely)
                {
                    return null;
                }

                return await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());
            }

            return default;
        }
    }
}