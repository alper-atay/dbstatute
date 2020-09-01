using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Builders
{
    public interface IModelBuilder : IBuilder
    {
        IFieldQualifier FieldQualifier { get; }
        IPredicateFieldQualifier PredicateFieldQualifier { get; }
        IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public interface IModelBuilder<TModel> : IBuilder<TModel>, IModelBuilder
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}