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

        protected override Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            bool fieldQualifierEnabled = SelectProxy.SelectedFieldQualifierEnabled;
            bool orderFieldQualifierEnabled = SelectProxy.OrderFieldQualifierEnabled;

            IFieldQualifier<TModel> selectedFieldQualifier = SelectProxy.SelectedFieldQualifier;
            IOrderFieldQualifier<TModel> orderFieldQualifier = SelectProxy.OrderFieldQualifier;
            IQueryGroupBuilder<TModel> selectQueryGroupBuilder = SelectProxy.QueryGroupBuilder;

            IEnumerable<Field> fields = null;
            if (fieldQualifierEnabled)
            {
                FieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(selectedFieldQualifier);
                fieldBuilder.Build(out fields);
            }

            IEnumerable<OrderField> orderFields = null;
            if (orderFieldQualifierEnabled)
            {
                OrderFieldBuilder<TModel> orderFieldBuilder = new OrderFieldBuilder<TModel>(orderFieldQualifier);
                orderFieldBuilder.Build(out orderFields);
            }




            return default;
        }
    }
}