using RepoDb;
using System;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying.Fundamentals.Qualifiers
{
    public interface IValueQualifier
    {
        bool IsSetted(Field field);

        bool Set(Field field, object value, bool overrideEnabled = false);

        bool Unset(Field field);
    }

    public interface IValueQualifier<TModel> : IValueQualifier
        where TModel : class, IModel, new()
    {
        bool IsSetted(Expression<Func<TModel, object>> expression);

        bool Set(Expression<Func<TModel, object>> expression, object value, bool overrideEnabled = false);

        bool Unset(Expression<Func<TModel, object>> expression);
    }
}