using DbStatute.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying
{
    public interface IFieldQualifier
    {
        private static readonly IFieldQualifier _empty;

        static IFieldQualifier()
        {
            _empty = new FieldQualifier(Enumerable.Empty<Field>());
        }

        static IFieldQualifier Empty => _empty;

        IEnumerable<Field> Fields { get; }
    }

    public interface IFieldQualifier<TId, TModel> : IFieldQualifier
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        bool IsFieldSetted(Expression<Func<TModel, object>> property);

        bool IsFieldSetted(string propertyName, Type propertyType = null);

        bool SetField(Expression<Func<TModel, object>> property, bool overrideEnabled = false);

        bool SetField(string propertyName, Type propertyType = null, bool overrideEnabled = false);

        bool UnsetField(Expression<Func<TModel, object>> property);

        bool UnsetField(string propertyName, Type propertyType = null);

        bool UnsetField(string propertyName);
    }
}