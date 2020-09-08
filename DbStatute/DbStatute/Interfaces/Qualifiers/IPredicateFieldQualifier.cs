using Basiclog;
using DbStatute.Interfaces.Fundamentals.Enumerables;
using DbStatute.Interfaces.Utilities;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IPredicateFieldQualifier : ISettableSpecializedField<ReadOnlyLogbookPredicate<object>>, IFieldPredicatePairs
    {
    }

    public interface IPredicateFieldQualifier<TModel> : ISettableSpecializedField<TModel, ReadOnlyLogbookPredicate<object>>, IPredicateFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}