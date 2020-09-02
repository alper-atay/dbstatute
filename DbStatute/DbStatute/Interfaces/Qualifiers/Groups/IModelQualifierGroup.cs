namespace DbStatute.Interfaces.Qualifiers.Groups
{
    public interface IModelQualifierGroup
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }

        IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public interface IModelQualifierGroup<TModel> : IModelQualifierGroup
    where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}