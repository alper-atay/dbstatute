using DbStatute.Interfaces.Fundamentals.Enumerables;
using DbStatute.Interfaces.Utilities;
using RepoDb;
using System;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IValueFieldQualifier : ISettableSpecializedField<object>, IFieldValuePairs
    {
        object GetValue(Field field);
    }

    public interface IValueFieldQualifier<TModel> : ISettableSpecializedField<TModel, object>, IValueFieldQualifier
        where TModel : class, IModel, new()
    {
        object GetValue(Expression<Func<TModel, object>> expression);
    }
}