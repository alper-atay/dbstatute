using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Queries
{
    public interface IModelQuery
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }

        IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public interface IModelQuery<TModel> : IModelQuery
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}