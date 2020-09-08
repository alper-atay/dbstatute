using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Queries
{
    public interface ISearchQuery
    {
        IFieldQualifier Fields { get; }

        IOperationFieldQualifier OperationMap { get; }

        IPredicateFieldQualifier PredicateMap { get; }

        IValueFieldQualifier ValueMap { get; }
    }

    public interface ISearchQuery<TModel> : ISearchQuery
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> Fields { get; }

        new IOperationFieldQualifier<TModel> OperationMap { get; }

        new IPredicateFieldQualifier<TModel> PredicateMap { get; }

        new IValueFieldQualifier<TModel> ValueMap { get; }
    }
}