using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeByRawModels : IMultipleMergeBase, IRawModels
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface IMultipleMergeByRawModels<TModel> : IMultipleMergeBase<TModel>, IRawModels<TModel>, IMultipleMergeByRawModels
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}