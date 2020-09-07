using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertOnSourceModel : ISingleInsertBase, ISourceModel
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleInsertOnSourceModel<TModel> : ISingleInsertBase<TModel>, ISourceModel<TModel>, ISingleInsertOnSourceModel
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}