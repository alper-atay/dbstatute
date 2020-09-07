using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsert : ISingleInsertBase, ISourceModel
    {
    }

    public interface ISingleInsert<TModel> : ISingleInsertBase<TModel>, ISourceModel<TModel>, ISingleInsert
        where TModel : class, IModel, new()
    {
    }
}