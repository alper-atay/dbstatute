using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers
{
    public class OrderFieldQualifier : IOrderFieldQualifier
    {
        public OrderFieldQualifier(IEnumerable<OrderField> orderFields)
        {
            OrderFields = orderFields ?? throw new ArgumentNullException(nameof(orderFields));
        }

        public IEnumerable<OrderField> OrderFields { get; }
    }

    public class OrderFieldQualifier<TModel> : IOrderFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly HashSet<OrderField> _orderFields = new HashSet<OrderField>();

        public IEnumerable<OrderField> OrderFields => _orderFields.Count > 0 ? _orderFields : null;

        public bool IsOrderFieldSetted(Expression<Func<TModel, object>> property)
        {
            string propertyName = property.ToMember().GetName();

            return _orderFields.Count(x => x.Name == propertyName) > 0;
        }

        public bool IsOrderFieldSetted(string propertyName)
        {
            return _orderFields.Count(x => x.Name == propertyName) > 0;
        }

        public bool SetOrderField(Expression<Func<TModel, object>> property, Order order, bool overrideEnabled = false)
        {
            if (IsOrderFieldSetted(property))
            {
                if (overrideEnabled)
                {
                    UnsetOrderField(property);
                }
                else
                {
                    return false;
                }
            }

            OrderField orderField = OrderField.Parse(property, order);

            return _orderFields.Add(orderField);
        }

        public bool SetOrderField(string propertyName, Order order, bool overrideEnabled = false)
        {
            if (IsOrderFieldSetted(propertyName))
            {
                if (overrideEnabled)
                {
                    UnsetOrderField(propertyName);
                }
                else
                {
                    return false;
                }
            }

            OrderField orderField = new OrderField(propertyName, order);

            return _orderFields.Add(orderField);
        }

        public bool UnsetOrderField(Expression<Func<TModel, object>> property)
        {
            string propertyName = property.ToMember().GetName();

            return _orderFields.RemoveWhere(x => x.Name == propertyName) > 0;
        }

        public bool UnsetOrderField(string propertyName)
        {
            return _orderFields.RemoveWhere(x => x.Name == propertyName) > 0;
        }
    }
}