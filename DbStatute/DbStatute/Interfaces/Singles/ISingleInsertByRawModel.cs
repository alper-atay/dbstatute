using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByRawModel<TFieldQualifier, TPredicateFieldQualifier> : ISingleInsertBase, IRawModel
        where TFieldQualifier : IFieldQualifier
        where TPredicateFieldQualifier : IPredicateFieldQualifier
    {
        TFieldQualifier FieldQualifier { get; }
        TPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleInsertByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier> : ISingleInsertBase<TModel>, IRawModel<TModel>, ISingleInsertByRawModel<TFieldQualifier, TPredicateFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : IFieldQualifier<TModel>
        where TPredicateFieldQualifier : IPredicateFieldQualifier<TModel>
    {
        new TFieldQualifier FieldQualifier { get; }
        new TPredicateFieldQualifier PredicateFieldQualifier { get; }
    }
}