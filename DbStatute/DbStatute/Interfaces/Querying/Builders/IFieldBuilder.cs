using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IFieldBuilder : IBuilder
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IFieldBuilder<TModel> : IBuilder<IEnumerable<Field>>, IFieldBuilder
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}