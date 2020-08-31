﻿using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Querying.Builders
{
    public class OrderFieldBuilder<TModel> : Builder<IEnumerable<OrderField>>, IOrderFieldBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public OrderFieldBuilder()
        {
            OrderFieldQualifier = new OrderFieldQualifier<TModel>();
        }

        public OrderFieldBuilder(IOrderFieldQualifier<TModel> orderFieldQualifier)
        {
            OrderFieldQualifier = orderFieldQualifier ?? throw new ArgumentNullException(nameof(orderFieldQualifier));
        }

        public IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        IOrderFieldQualifier IOrderFieldBuilder.OrderFieldQualifier => OrderFieldQualifier;

        protected override bool BuildOperation(out IEnumerable<OrderField> built)
        {
            built = null;

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

                built = OrderFieldQualifier.OrderFields;
            }

            return !(built is null);
        }
    }
}