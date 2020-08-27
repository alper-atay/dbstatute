using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByRawModel : ISingleUpdateBase, IRawModel
    {
    }

    public interface ISingleUpdateByRawModel<TModel> : ISingleUpdateBase<TModel>, IRawModel<TModel>, ISingleUpdateByRawModel
        where TModel : class, IModel, new()
    {
    }
}