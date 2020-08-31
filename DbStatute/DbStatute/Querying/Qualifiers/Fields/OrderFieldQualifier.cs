using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class OrderFieldQualifier : IOrderFieldQualifier
    {
        protected readonly HashSet<OrderField> _orderFields;

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

        public IEnumerable<OrderField> GetAllByField(Field field)
        {
            return _orderFields.Where(x => x.Name == field.Name);
        }

        public IEnumerable<OrderField> GetAllByName(string name)
        {
            return _orderFields.Where(x => x.Name == name);
        }

        public IEnumerable<OrderField> GetAllByOrder(Order order)
        {
            return _orderFields.Where(x => x.Order == order);
        }

        public bool IsSetted(OrderField orderField)
        {
            return _orderFields.Contains(orderField);
        }

        public int IsSetted(Field field)
        {
            return _orderFields.Count(x => x.Equals(field));
        }

        public bool Set(OrderField orderField, bool overrideEnabled = false)
        {
            if (!_orderFields.Add(orderField))
            {
                if (overrideEnabled)
                {
                    _orderFields.Remove(orderField);

                    return _orderFields.Add(orderField);
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

        public int Unset(Field field)
        {
            return _orderFields.RemoveWhere(x => x.Name == field.Name);
        }

        public int Unset(string name)
        {
            return _orderFields.RemoveWhere(x => x.Name == name);
        }
    }

    public class OrderFieldQualifier<TModel> : OrderFieldQualifier, IOrderFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public OrderFieldQualifier()
        {
        }

        public OrderFieldQualifier(IEnumerable<OrderField> orderFields) : base(orderFields)
        {
        }

        public int IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (var field in fields)
            {
                settedCount += IsSetted(field);
            }

            return settedCount;
        }

        public int Set(Expression<Func<TModel, object>> expression, Order order, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (Field field in fields)
            {
                OrderField orderField = new OrderField(field.Name, order);

                if (Set(orderField, overrideEnabled))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int Unset(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int unsettedCount = 0;

            foreach (Field field in fields)
            {
                unsettedCount += Unset(field);
            }

            return unsettedCount;
        }
    }
}