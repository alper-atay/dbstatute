using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertBySourceModels : IMultipleInsertBase, ISourceModels
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface IMultipleInsertBySourceModels<TModel> : IMultipleInsertBase<TModel>, ISourceModels<TModel>, IMultipleInsertBySourceModels
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}