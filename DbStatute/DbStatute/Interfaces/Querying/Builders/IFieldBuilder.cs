using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IFieldBuilder
    {
        IFieldQualifier FieldQualifier { get; }

        bool BuildFields(out IEnumerable<Field> fields);
    }

    public interface IFieldBuilder<TModel> : IFieldBuilder
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}