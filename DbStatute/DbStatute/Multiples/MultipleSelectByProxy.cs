using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Proxies;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleSelectByProxy<TModel> : MultipleSelectBase<TModel>, IMultipleSelectByProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleSelectByProxy()
        {
            SelectProxy = new SelectProxy<TModel>();
        }

        public MultipleSelectByProxy(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy IMultipleSelectByProxy.SelectProxy => SelectProxy;

        protected override async IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(SelectProxy.WhereQuery.Build<TModel>(Conjunction.And, out QueryGroup queryGroup));

            if (ReadOnlyLogs.Safely)
            {
                bool selectedFieldsBuilt = SelectProxy.SelectedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);
                bool selectedOrderFieldsBuilt = SelectProxy.OrderFieldQualifier.Build<TModel>(out IEnumerable<OrderField> orderFields);

                if (selectedFieldsBuilt)
                {
                    fields = null;
                }

                if (selectedOrderFieldsBuilt)
                {
                    orderFields = null;
                }

                IEnumerable<TModel> selectedModels = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                foreach (TModel selectedModel in selectedModels)
                {
                    yield return selectedModel;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(SelectProxy.WhereQuery.Build<TModel>(Conjunction.And, out QueryGroup queryGroup));

            if (ReadOnlyLogs.Safely)
            {
                bool fieldsBuilt = SelectProxy.SelectedFieldQualifier.Build<TModel>(out IEnumerable<Field> fields);
                bool orderFieldsBuilt = SelectProxy.OrderFieldQualifier.Build<TModel>(out IEnumerable<OrderField> orderFields);

                if (fieldsBuilt)
                {
                    fields = null;
                }

                if (orderFieldsBuilt)
                {
                    orderFields = null;
                }

                IEnumerable<TModel> selectedModels = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                return selectedModels;
            }

            return null;
        }
    }
}