using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Utilities
{
    public interface ISettableField
    {
        IEnumerable<Field> GetAllByName(string name);

        IEnumerable<Field> GetAllByType(Type type);

        IEnumerable<Field> GetAllByType<T>();

        bool IsSetted(Field field);

        int IsSetted(string name);

        bool Set(Field field, bool overrideEnabled = false);

        bool Unset(Field field);
    }

    public interface ISettableField<TModel> : ISettableField
        where TModel : class, IModel, new()
    {
        int IsSetted(Expression<Func<TModel, object>> expression);

        int Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false);

        int Unset(Expression<Func<TModel, object>> expression);
    }
}