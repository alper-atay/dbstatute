using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying;
using DbStatute.Querying.Builders;
using RepoDb;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleSelectByProxy<TModel, TSelectProxy> : MultipleSelectBase<TModel>, IMultipleSelectByProxy<TModel, TSelectProxy>
        where TModel : class, IModel, new()
        where TSelectProxy : class, ISelectProxy<TModel>
    {
        public MultipleSelectByProxy()
        {
            SelectProxy = new SelectProxy<TModel>() as TSelectProxy;
        }

        public MultipleSelectByProxy(TSelectProxy selectProxy)
        {
            SelectProxy = selectProxy;
        }

        public TSelectProxy SelectProxy { get; }

        protected override async IAsyncEnumerable<TModel> SelectAsSignlyOperationAsync(IDbConnection dbConnection)
        {
            if (SelectProxy.SelectQueryGroupBuilder.Build(out QueryGroup queryGroup))
            {
                IFieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(SelectProxy.SelectedFieldQualifier);
                IOrderFieldBuilder<TModel> orderFieldBuilder = new OrderFieldBuilder<TModel>(SelectProxy.OrderFieldQualifier);

                fieldBuilder.Build(out IEnumerable<Field> fields);
                orderFieldBuilder.Build(out IEnumerable<OrderField> orderFields);

                IEnumerable<TModel> selectedModels = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                foreach (TModel selectedModel in selectedModels)
                {
                    yield return selectedModel;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (SelectProxy.SelectQueryGroupBuilder.Build(out QueryGroup queryGroup))
            {
                IFieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(SelectProxy.SelectedFieldQualifier);
                IOrderFieldBuilder<TModel> orderFieldBuilder = new OrderFieldBuilder<TModel>(SelectProxy.OrderFieldQualifier);

                fieldBuilder.Build(out IEnumerable<Field> fields);
                orderFieldBuilder.Build(out IEnumerable<OrderField> orderFields);

                IEnumerable<TModel> selectedModels = await dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, MaxSelectCount, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder);

                return selectedModels;
            }

            return null;
        }
    }
}