using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsert : ISingleInsertBase, ISourceableModelQuery
    {
    }

    public interface ISingleInsert<TModel> : ISingleInsertBase<TModel>, ISourceableModelQuery<TModel>, ISingleInsert
        where TModel : class, IModel, new()
    {
    }
}