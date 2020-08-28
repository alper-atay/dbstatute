using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Querying.Builders
{
    public class OrderFieldBuilder : IOrderFieldBuilder
    {
        public IOrderFieldQualifier OrderFieldQualifier { get; }

        public bool Build(out IEnumerable<OrderField> orderFields)
        {
            orderFields = null;

            if (OrderFieldQualifier.HasOrderField)
            {
                orderFields = OrderFieldQualifier.OrderFields;
            }

            return !(orderFields is null);
        }
    }

    public class OrderFieldBuilder<TModel> : IOrderFieldBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        IOrderFieldQualifier IOrderFieldBuilder.OrderFieldQualifier { get; }

        public bool Build(out IEnumerable<OrderField> builtOrderFields)
        {
            builtOrderFields = null;

            if (OrderFieldQualifier.HasOrderField)
            {
                HashSet<Field> modelFields = Field.Parse<TModel>().ToHashSet();

                IEnumerable<OrderField> orderFields = OrderFieldQualifier.OrderFields;
                string[] orderFieldNames = orderFields.Select(x => x.Name).ToArray();
                IEnumerable<Field> fields = Field.From(orderFieldNames).ToHashSet();

                if (!modelFields.IsSupersetOf(fields))
                {
                    throw new InvalidOperationException("Model fields must be superset of qualifier fields");
                }

                builtOrderFields = OrderFieldQualifier.OrderFields;
            }

            return !(builtOrderFields is null);
        }
    }
}