using DbStatute.Interfaces.Utilities;
using RepoDb;
using System;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Utilities
{
    public interface ISettableSpecializedField<TValue> : ISettableField
    {
        bool Set(Field field, TValue value, bool overrideEnabled = false);
    }

    public interface ISettableSpecializedField<TModel, TValue> : ISettableField<TModel>, ISettableSpecializedField<TValue>
        where TModel : class, IModel, new()
    {
        int Set(Expression<Func<TModel, object>> expression, TValue value, bool overrideEnabled = false);
    }
}