using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByRawModel<TFieldQualifier> : ISingleInsertBase, IRawModel
        where TFieldQualifier : IFieldQualifier
    {
        TFieldQualifier FieldQualifier { get; }
    }

    public interface ISingleInsertByRawModel<TModel, TFieldQualifier> : ISingleInsertBase<TModel>, IRawModel<TModel>, ISingleInsertByRawModel<TFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : IFieldQualifier<TModel>
    {
        new TFieldQualifier FieldQualifier { get; }
    }
}