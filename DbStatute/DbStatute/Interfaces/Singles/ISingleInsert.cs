using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsert : ISingleInsertBase, ISourceableQuery
    {
    }

    public interface ISingleInsert<TModel> : ISingleInsertBase<TModel>, ISourceableQuery<TModel>, ISingleInsert
        where TModel : class, IModel, new()
    {
    }
}