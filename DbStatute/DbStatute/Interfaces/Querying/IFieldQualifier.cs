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
        void SetField<TProperty>(Expression<Func<TModel, TProperty>> property, Type type = null);

        bool UnsetField<TProperty>(Expression<Func<TModel, TProperty>> property);
    }
}