using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Fundamentals;

namespace DbStatute.Interfaces.Querying
{
    public interface IInsertQuery : IStatuteQueryBase
    {
        IMergeQueryGroupBuilder MergeQueryGroupBuilder { get; }
    }

    public interface IInsertQuery<TModel> : IStatuteQueryBase<TModel>, IInsertQuery
        where TModel : class, IModel, new()
    {
        new IMergeQueryGroupBuilder<TModel> MergeQueryGroupBuilder { get; }
    }
}