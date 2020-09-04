using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Utilities
{
    public interface ISettableQueryField
    {
        int IsSetted(Field field);

        bool IsSetted(QueryField queryField);

        bool Set(QueryField queryField, bool overrideEnabled = false);

        bool Set(Field field, bool overrideEnabled = false);

        bool Set(Field field, object value, bool overrideEnabled = false);

        bool Set(Field field, Operation operation, bool overrideEnabled = false);

        bool Set(Field field, Operation operation, object value, bool overrideEnabled = false);

        int SettedCount(Field field);

        int Unset(Field field);

        bool Unset(QueryField queryField);

        int UnsetAll();
    }

    public interface ISettableQueryField<TModel> : ISettableQueryField
        where TModel : class, IModel, new()
    {
        int IsSetted(Expression<Func<TModel, object>> expression);

        int Set(Expression<Func<TModel, object>> expression, object value, bool overrideEnabled = false);

        int Set(Expression<Func<TModel, object>> expression, Operation operation, bool overrideEnabled = false);

        int Set(Expression<Func<TModel, object>> expression, Operation operation, object value, bool overrideEnabled = false);

        int Unset(Expression<Func<TModel, object>> expression);
    }
}