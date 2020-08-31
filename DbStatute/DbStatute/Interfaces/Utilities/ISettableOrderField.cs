using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Utilities
{
    public interface ISettableOrderField
    {
        IEnumerable<OrderField> GetAllByField(Field field);

        IEnumerable<OrderField> GetAllByName(string name);

        IEnumerable<OrderField> GetAllByOrder(Order order);

        int IsSetted(Field field);

        bool IsSetted(OrderField orderField);

        bool Set(OrderField orderField, bool overrideEnabled = false);

        bool Set(Field field, Order order, bool overrideEnabled = false);

        int Unset(Field field);

        int Unset(string name);

        bool Unset(OrderField orderField);
    }

    public interface ISettableOrderField<TModel> : ISettableOrderField
        where TModel : class, IModel, new()
    {
        int IsSetted(Expression<Func<TModel, object>> expression);

        int Set(Expression<Func<TModel, object>> expression, Order order, bool overrideEnabled = false);

        int Unset(Expression<Func<TModel, object>> expression);
    }
}