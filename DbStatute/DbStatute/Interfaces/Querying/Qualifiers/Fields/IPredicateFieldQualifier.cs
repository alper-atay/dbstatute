using Basiclog;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IPredicateFieldQualifier : ISettableSpecializedField<ReadOnlyLogbookPredicate<object>>, IFieldPredicateMap
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IPredicateFieldQualifier<TModel> : ISettableSpecializedField<TModel, ReadOnlyLogbookPredicate<object>>, IPredicateFieldQualifier
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}