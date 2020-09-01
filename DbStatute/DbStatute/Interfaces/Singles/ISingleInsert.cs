using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsert : ISingleInsertBase, IReadyModel
    {
    }

    public interface ISingleInsert<TModel> : ISingleInsertBase<TModel>, IReadyModel<TModel>, ISingleInsert
        where TModel : class, IModel, new()
    {
    }
}