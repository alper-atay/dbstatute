using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying
{
    public interface IFieldQualifier
    {
        IEnumerable<Field> Fields { get; }
    }

    public interface IFieldQualifier<TId, TModel> : IFieldQualifier
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        bool IsFieldSetted<TValue>(Expression<Func<TModel, TValue>> property);

        void SetField<TProperty>(Expression<Func<TModel, TProperty>> property);

        bool UnsetField<TProperty>(Expression<Func<TModel, TProperty>> property);
    }
}