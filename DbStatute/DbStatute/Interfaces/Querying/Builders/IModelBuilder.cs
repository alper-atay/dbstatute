using Basiclog;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IModelBuilder
    {
        IValueFieldQualifier ValueFieldQualifier { get; }

        IReadOnlyLogbook BuildModel(out object model);
    }

    public interface IModelBuilder<TModel> : IModelBuilder
        where TModel : class, IModel, new()
    {
        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }

        IReadOnlyLogbook BuildModel(out TModel model);
    }
}