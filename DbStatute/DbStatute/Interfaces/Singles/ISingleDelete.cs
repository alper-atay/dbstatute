using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDelete : ISingleDeleteBase, ISourceModel
    {
    }

    public interface ISingleDelete<TModel> : ISingleDeleteBase<TModel>, ISourceModel<TModel>
        where TModel : class, IModel, new()
    {
    }
}