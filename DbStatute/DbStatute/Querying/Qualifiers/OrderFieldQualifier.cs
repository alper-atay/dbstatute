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

        public bool HasOrderField => OrderFields.Count() > 0;
        public IEnumerable<OrderField> OrderFields { get; }
    }

    public class OrderFieldQualifier<TModel> : IOrderFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly HashSet<OrderField> _orderFields = new HashSet<OrderField>();

        public bool HasOrderField => _orderFields.Count > 0;
        public IEnumerable<OrderField> OrderFields => _orderFields.Count > 0 ? _orderFields : null;

        public bool IsOrderFieldSetted(Expression<Func<TModel, object>> expression)
        {
            string propertyName = expression.ToMember().GetName();

            return _orderFields.Count(x => x.Name == propertyName) > 0;
        }

        public bool IsOrderFieldSetted(string name)
        {
            return _orderFields.Count(x => x.Name == name) > 0;
        }

        public bool SetOrderField(Expression<Func<TModel, object>> expression, Order order, bool overrideEnabled = false)
        {
            if (IsOrderFieldSetted(expression))
            {
                if (overrideEnabled)
                {
                    UnsetOrderField(expression);
                }
                else
                {
                    return false;
                }
            }

            OrderField orderField = OrderField.Parse(expression, order);

            return _orderFields.Add(orderField);
        }

        public bool SetOrderField(string name, Order order, bool overrideEnabled = false)
        {
            if (IsOrderFieldSetted(name))
            {
                if (overrideEnabled)
                {
                    UnsetOrderField(name);
                }
                else
                {
                    return false;
                }
            }

            OrderField orderField = new OrderField(name, order);

            return _orderFields.Add(orderField);
        }

        public bool UnsetOrderField(Expression<Func<TModel, object>> expression)
        {
            string propertyName = expression.ToMember().GetName();

            return _orderFields.RemoveWhere(x => x.Name == propertyName) > 0;
        }

        public bool UnsetOrderField(string name)
        {
            return _orderFields.RemoveWhere(x => x.Name == name) > 0;
        }
    }
}