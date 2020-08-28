using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class OrderFieldQualifier : IOrderFieldQualifier
    {
        private readonly HashSet<OrderField> _orderFields;

        public OrderFieldQualifier()
        {
            _orderFields = new HashSet<OrderField>();
        }

        public OrderFieldQualifier(IEnumerable<OrderField> orderFields)
        {
            if (orderFields is null)
            {
                throw new ArgumentNullException(nameof(orderFields));
            }

            _orderFields = new HashSet<OrderField>(orderFields);
        }

        public bool HasOrderField => _orderFields.Count > 0;
        public IEnumerable<OrderField> OrderFields => _orderFields;

        public bool IsSetted(OrderField orderField)
        {
            return _orderFields.Contains(orderField);
        }

        public bool Set(OrderField orderField, bool overrideEnabled = false)
        {
            if (!_orderFields.Add(orderField))
            {
                if (overrideEnabled)
                {
                    _orderFields.Remove(orderField);
                    _orderFields.Add(orderField);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Unset(OrderField orderField)
        {
            return _orderFields.Remove(orderField);
        }
    }

    public class OrderFieldQualifier<TModel> : IOrderFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private static readonly EqualityComparer _EqualityComparer = new EqualityComparer();

        private readonly HashSet<OrderField> _orderFields = new HashSet<OrderField>();

        public OrderFieldQualifier()
        {
            _orderFields = new HashSet<OrderField>();
        }

        public OrderFieldQualifier(IEnumerable<OrderField> orderFields)
        {
            if (orderFields is null)
            {
                throw new ArgumentNullException(nameof(orderFields));
            }

            _orderFields = new HashSet<OrderField>(orderFields);
        }

        public bool HasOrderField => _orderFields.Count > 0;
        public IEnumerable<OrderField> OrderFields => _orderFields.Count > 0 ? _orderFields : null;

        public bool IsSetted(Expression<Func<TModel, object>> expression)
        {
            Field field = expression?.ToMember().GetField();
            var fieldName = field?.Name;

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new NullReferenceException();
            }

            return _orderFields.Count(x => x.Name == field.Name) > 0;
        }

        public bool IsSetted(OrderField orderField)
        {
            return _orderFields.Contains(orderField);
        }

        public bool Set(Expression<Func<TModel, object>> expression, Order order, bool overrideEnabled = false)
        {
            OrderField orderField = OrderField.Parse(expression, order);

            if (!_orderFields.Add(orderField))
            {
                if (overrideEnabled)
                {
                    _orderFields.Remove(orderField);
                    _orderFields.Add(orderField);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Set(OrderField orderField, bool overrideEnabled = false)
        {
            if (!_orderFields.Add(orderField))
            {
                if (overrideEnabled)
                {
                    _orderFields.Remove(orderField); //Remove on hash code
                    _orderFields.Add(orderField); //Add with reference. Reference changed
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Unset(Expression<Func<TModel, object>> expression)
        {
            Field field = expression?.ToMember().GetField();
            var fieldName = field?.Name;

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new NullReferenceException();
            }

            return _orderFields.RemoveWhere(x => x.Name == fieldName) > 0;
        }

        public bool Unset(OrderField orderField)
        {
            return _orderFields.RemoveWhere(x => x.Name == orderField.Name) > 0;
        }

        private sealed class EqualityComparer : IEqualityComparer<OrderField>
        {
            public bool Equals(OrderField x, OrderField y)
            {
                return x.Name == y.Name && x.Order == y.Order;
            }

            public int GetHashCode(OrderField obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}