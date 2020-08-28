using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying.Builders;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByRawModelQuery<TFieldQualifier> : ISingleInsertBase, IRawModel
        where TFieldQualifier : IFieldBuilder
    {
        TFieldQualifier FieldQualifier { get; }
    }

    public interface ISingleInsertByRawModelQuery<TModel, TFieldQualifier> : ISingleInsertBase<TModel>, IRawModel<TModel>, ISingleInsertByRawModelQuery<TFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : IFieldBuilder<TModel>
    {
        new TFieldQualifier FieldQualifier { get; }
    }
}