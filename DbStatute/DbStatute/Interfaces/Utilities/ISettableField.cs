using RepoDb;
using System;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Utilities
{
    public interface ISettableField
    {
        bool IsSetted(Field field);

        bool Set(Field field, bool overrideEnabled = false);

        bool Unset(Field field);
    }

    public interface ISettableField<TModel> : ISettableField
        where TModel : class, IModel, new()
    {
        bool IsSetted(Expression<Func<TModel, object>> expression);

        bool Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false);

        bool Unset(Expression<Func<TModel, object>> expression);
    }
}