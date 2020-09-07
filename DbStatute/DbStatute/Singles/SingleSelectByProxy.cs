using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
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
            Logs.AddRange(SelectProxy.WhereQuery.Build(Conjunction.And, out QueryGroup queryGroup));

            if (ReadOnlyLogs.Safely)
            {
                IFieldQualifier<TModel> selectedFieldQualifier = SelectProxy.SelectedFieldQualifier;
                IOrderFieldQualifier<TModel> orderFieldQualifier = SelectProxy.OrderFieldQualifier;

                bool fieldsBuilt = selectedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);
                bool orderFieldsBuilt = orderFieldQualifier.Build<TModel>(out IEnumerable<OrderField> orderFields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                if (!orderFieldsBuilt)
                {
                    orderFields = null;
                }

                return await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}