using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
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
        public MultipleSelectByProxy(ISelectProxy<TModel> selectProxy)
        {
            SelectProxy = selectProxy ?? throw new ArgumentNullException(nameof(selectProxy));
        }

        public ISelectProxy<TModel> SelectProxy { get; }

        ISelectProxy IMultipleSelectByProxy.SelectProxy => SelectProxy;

        protected override async IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection)
        {
            var selectedModels = await SelectAsync(dbConnection);

            if (IsSucceed)
            {
                foreach (var selectedModel in selectedModels)
                {
                    yield return selectedModel;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
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

                return await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);
            }

            return null;
        }
    }
}