using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IFieldBuilder
    {
        IFieldQualifier FieldQualifier { get; }

        bool Build(out IEnumerable<Field> builtFields);
    }

    public interface IFieldBuilder<TModel> : IFieldBuilder
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}