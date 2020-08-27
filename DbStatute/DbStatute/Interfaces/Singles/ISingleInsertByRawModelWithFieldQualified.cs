using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByRawModelWithFieldQualifier<TFieldQualifier> : ISingleInsertBase, IRawModel
        where TFieldQualifier : IFieldQualifier
    {
        TFieldQualifier FieldQualifier { get; }
    }

    public interface ISingleInsertByRawModelWithFieldQualifier<TModel, TFieldQualifier> : ISingleInsertBase<TModel>, IRawModel<TModel>, ISingleInsertByRawModelWithFieldQualifier<TFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : class, IFieldQualifier<TModel>
    {
        new TFieldQualifier FieldQualifier { get; }
    }
}