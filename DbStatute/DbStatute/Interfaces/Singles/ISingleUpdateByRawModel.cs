using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByRawModel<TFieldQualifier, TPredicateFieldQualifier> : ISingleUpdateBase, IRawModel
        where TFieldQualifier : IFieldQualifier
        where TPredicateFieldQualifier : IPredicateFieldQualifier
    {
        TFieldQualifier FieldQualifier { get; }

        TPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleUpdateByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier> : ISingleUpdateBase<TModel>, IRawModel<TModel>, ISingleUpdateByRawModel<TFieldQualifier, TPredicateFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : IFieldQualifier<TModel>
        where TPredicateFieldQualifier : IPredicateFieldQualifier<TModel>
    {
        new TFieldQualifier FieldQualifier { get; }

        new TPredicateFieldQualifier PredicateFieldQualifier { get; }
    }
}