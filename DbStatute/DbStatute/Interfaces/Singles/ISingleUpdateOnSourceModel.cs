using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateOnSourceModel : ISingleUpdateBase, ISourceModel
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface ISingleUpdateOnSourceModel<TModel> : ISingleUpdateBase<TModel>, ISourceModel<TModel>, ISingleUpdateOnSourceModel
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}