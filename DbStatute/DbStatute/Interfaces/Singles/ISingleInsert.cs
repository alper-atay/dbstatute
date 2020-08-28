using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsert : ISingleInsertBase, IRawModel
    {
    }

    public interface ISingleInsert<TModel> : ISingleInsertBase<TModel>, IRawModel<TModel>, ISingleInsert
        where TModel : class, IModel, new()
    {
    }
}