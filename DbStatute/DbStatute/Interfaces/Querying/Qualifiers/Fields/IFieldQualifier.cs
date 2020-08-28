using DbStatute.Interfaces.Utilities;
using DbStatute.Querying.Qualifiers;
using RepoDb;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IFieldQualifier : ISettableField
    {
        private static readonly IFieldQualifier _empty;

        static IFieldQualifier()
        {
            _empty = new FieldQualifier(Enumerable.Empty<Field>());
        }

        static IFieldQualifier Empty => _empty;

        IEnumerable<Field> Fields { get; }
        bool HasField { get; }
    }

    public interface IFieldQualifier<TModel> : ISettableField<TModel>, IFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}