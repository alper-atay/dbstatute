using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class OrderFieldQualifier : IOrderFieldQualifier
    {
        private readonly HashSet<OrderField> _data = new HashSet<OrderField>();

        public bool HasOrderField => _data.Count > 0;

        public IEnumerable<OrderField> GetAllByField(Field field)
        {
            return _data.Where(x => x.Name.Equals(field.Name));
        }

        public IEnumerable<OrderField> GetAllByName(string name)
        {
            return _data.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<OrderField> GetAllByOrder(Order order)
        {
            return _data.Where(x => x.Order.Equals(order));
        }

        public IEnumerator<OrderField> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public bool IsSetted(OrderField orderField)
        {
            return _data.Contains(orderField);
        }

        public int IsSetted(Field field)
        {
            return _data.Count(x => x.Name.Equals(field.Name));
        }

        public bool Set(OrderField orderField, bool overrideEnabled = false)
        {
            if (!_data.Add(orderField))
            {
                if (overrideEnabled)
                {
                    _data.Remove(orderField);

                    return _data.Add(orderField);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Set(Field field, Order order, bool overrideEnabled = false)
        {
            int isSetted = IsSetted(field);

            if (isSetted > 0)
            {
                if (overrideEnabled)
                {
                    Unset(field);
                    OrderField orderField = new OrderField(field.Name, order);

                    return _data.Add(orderField);
                }
            }

            return false;
        }

        public bool Unset(OrderField orderField)
        {
            return _data.Remove(orderField);
        }

        public int Unset(Field field)
        {
            return _data.RemoveWhere(x => x.Name.Equals(field.Name));
        }

        public int Unset(string name)
        {
            return _data.RemoveWhere(x => x.Name.Equals(name));
        }
    }

    public class OrderFieldQualifier<TModel> : OrderFieldQualifier, IOrderFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public int IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (Field field in fields)
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
                if (Set(field, order, overrideEnabled))
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