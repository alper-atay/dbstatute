using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class OrderFieldQualifier : IOrderFieldQualifier
    {
        public bool HasOrderField => OrderFields.Count > 0;
        public IEnumerable<OrderField> ReadOnlyOrderFields => OrderFields;
        protected HashSet<OrderField> OrderFields { get; } = new HashSet<OrderField>();

        public IEnumerable<OrderField> GetAllByField(Field field)
        {
            return OrderFields.Where(x => x.Name.Equals(field.Name));
        }

        public IEnumerable<OrderField> GetAllByName(string name)
        {
            return OrderFields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<OrderField> GetAllByOrder(Order order)
        {
            return OrderFields.Where(x => x.Order.Equals(order));
        }

        public bool IsSetted(OrderField orderField)
        {
            return OrderFields.Contains(orderField);
        }

        public int IsSetted(Field field)
        {
            return OrderFields.Count(x => x.Name.Equals(field.Name));
        }

        public bool Set(OrderField orderField, bool overrideEnabled = false)
        {
            if (!OrderFields.Add(orderField))
            {
                if (overrideEnabled)
                {
                    OrderFields.Remove(orderField);

                    return OrderFields.Add(orderField);
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

                    return OrderFields.Add(orderField);
                }
            }

            return false;
        }

        public bool Unset(OrderField orderField)
        {
            return OrderFields.Remove(orderField);
        }

        public int Unset(Field field)
        {
            return OrderFields.RemoveWhere(x => x.Name.Equals(field.Name));
        }

        public int Unset(string name)
        {
            return OrderFields.RemoveWhere(x => x.Name.Equals(name));
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