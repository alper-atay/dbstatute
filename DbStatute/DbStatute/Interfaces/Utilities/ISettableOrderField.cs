using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Utilities
{
    public interface ISettableOrderField
    {
        bool IsSetted(OrderField orderField);

        bool Set(OrderField orderField, bool overrideEnabled = false);

        bool Unset(OrderField orderField);
    }

    public interface ISettableOrderField<TModel> : ISettableOrderField
        where TModel : class, IModel, new()
    {
        bool IsSetted(Expression<Func<TModel, object>> expression);

        bool Set(Expression<Func<TModel, object>> expression, Order order, bool overrideEnabled = false);

        bool Unset(Expression<Func<TModel, object>> expression);
    }
}