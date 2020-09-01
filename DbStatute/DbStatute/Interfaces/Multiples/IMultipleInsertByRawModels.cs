using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByRawModels : IMultipleInsertBase, IRawModels
    {
        IFieldQualifier FieldQualifier { get; }
        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface IMultipleInsertByRawModels<TModel> : IMultipleInsertBase<TModel>, IRawModels<TModel>, IMultipleInsertByRawModels
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}