using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByRawModel : ISingleInsertBase, IRawModel
    {
    }

    public interface ISingleInsertByRawModel<TModel> : ISingleInsertBase<TModel>, IRawModel<TModel>
        where TModel : class, IModel, new()
    {
    }
}