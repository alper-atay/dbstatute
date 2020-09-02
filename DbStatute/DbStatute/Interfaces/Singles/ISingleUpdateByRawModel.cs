using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByRawModel : ISingleUpdateBase, IRawModel
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleUpdateByRawModel<TModel> : ISingleUpdateBase<TModel>, IRawModel<TModel>, ISingleUpdateByRawModel
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}