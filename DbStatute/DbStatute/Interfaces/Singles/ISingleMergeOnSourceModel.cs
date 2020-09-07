using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeOnSourceModel : ISingleMergeBase, ISourceModel
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleMergeOnSourceModel<TModel> : ISingleMergeBase<TModel>, ISourceModel<TModel>, ISingleMergeOnSourceModel
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}