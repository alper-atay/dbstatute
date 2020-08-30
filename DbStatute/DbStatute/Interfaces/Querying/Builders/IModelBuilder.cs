using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IModelBuilder
    {
        IFieldBuilder FieldBuilder { get; }
        IPredicateFieldQualifier PredicateFieldQualifier { get; }
        IValueFieldQualifier ValueFieldQualifier { get; }

        bool Build(out dynamic model);
    }

    public interface IModelBuilder<TModel> : IModelBuilder
        where TModel : class, IModel, new()
    {
        new IFieldBuilder<TModel> FieldBuilder { get; }
        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }

        bool Build(out TModel model);
    }
}