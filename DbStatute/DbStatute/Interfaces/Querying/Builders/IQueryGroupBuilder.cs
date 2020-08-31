using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IQueryGroupBuilder
    {
        IFieldQualifier FieldQualifier { get; }
        IPredicateFieldQualifier PredicateFieldQualifier { get; }
        IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public interface IQueryGroupBuilder<TModel> : IQueryGroupBuilder
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}