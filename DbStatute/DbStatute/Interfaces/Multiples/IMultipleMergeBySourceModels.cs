using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeBySourceModels : IMultipleMergeBase, ISourceModels
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface IMultipleMergeBySourceModels<TModel> : IMultipleMergeBase<TModel>, ISourceModels<TModel>, IMultipleMergeBySourceModels
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}