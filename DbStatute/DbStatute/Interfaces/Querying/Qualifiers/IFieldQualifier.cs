using DbStatute.Querying.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IFieldQualifier
    {
        private static readonly IFieldQualifier _empty;

        static IFieldQualifier()
        {
            _empty = new FieldQualifier(Enumerable.Empty<Field>());
        }

        static IFieldQualifier Empty => _empty;

        bool HasField { get; }
        IEnumerable<Field> Fields { get; }
    }

    public interface IFieldQualifier<TModel> : IFieldQualifier
        where TModel : class, IModel, new()
    {
        bool IsFieldSetted(Expression<Func<TModel, object>> expression);

        bool IsFieldSetted(string name, Type type = null);

        bool SetField(Expression<Func<TModel, object>> expression, bool overrideEnabled = false);

        bool SetField(string name, Type type = null, bool overrideEnabled = false);

        bool UnsetField(Expression<Func<TModel, object>> expression);

        bool UnsetField(string name, Type type = null);

        bool UnsetField(string name);
    }
}