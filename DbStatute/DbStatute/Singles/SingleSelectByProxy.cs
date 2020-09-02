using DbStatute.Extensions;
using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
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
            ISelectQualifierGroup<TModel> selectQualifierGroup = SelectProxy.SelectQualifierGroup;

            Logs.AddRange(selectQualifierGroup.Build<TModel>(out QueryGroup queryGroup));

            if (ReadOnlyLogs.Safely)
            {
                IFieldQualifier<TModel> selectedFieldQualifier = SelectProxy.SelectedFieldQualifier;
                IOrderFieldQualifier<TModel> orderFieldQualifier = SelectProxy.OrderFieldQualifier;

                bool fieldsCreated = selectedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);
                bool orderFieldsCreated = orderFieldQualifier.Build<TModel>(out IEnumerable<OrderField> orderFields);

                if (!fieldsCreated)
                {
                    fields = null;
                }

                if (!orderFieldsCreated)
                {
                    orderFields = null;
                }

                return await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}