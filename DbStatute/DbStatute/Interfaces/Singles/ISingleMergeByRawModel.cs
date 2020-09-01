using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeByRawModel<TFieldQualifier, TPredicateFieldQualifier> : ISingleMergeBase, IRawModel
        where TFieldQualifier : IFieldQualifier
        where TPredicateFieldQualifier : IPredicateFieldQualifier
    {
        TFieldQualifier FieldQualifier { get; }
        TPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleMergeByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier> : ISingleMergeBase<TModel>, IRawModel<TModel>, ISingleMergeByRawModel<TFieldQualifier, TPredicateFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : IFieldQualifier<TModel>
        where TPredicateFieldQualifier : IPredicateFieldQualifier<TModel>
    {
        new TFieldQualifier FieldQualifier { get; }
        new TPredicateFieldQualifier PredicateFieldQualifier { get; }
    }
}