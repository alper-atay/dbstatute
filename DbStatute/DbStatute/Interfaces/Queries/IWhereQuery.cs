using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Queries
{
    public interface IWhereQuery
    {
        IFieldQualifier FieldQualifier { get; }

        IOperationFieldQualifier OperationFieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }

        IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public interface IWhereQuery<TModel> : IWhereQuery
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IOperationFieldQualifier<TModel> OperationFieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}