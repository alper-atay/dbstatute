using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Fundamentals;

namespace DbStatute.Interfaces.Querying
{
    public interface IUpdateQuery : IStatuteQueryBase
    {
        IMergeQueryGroupBuilder MergeQueryGroupBuilder { get; }
    }

    public interface IUpdateQuery<TModel> : IStatuteQueryBase<TModel>, IUpdateQuery
        where TModel : class, IModel, new()
    {
        new IMergeQueryGroupBuilder<TModel> MergeQueryGroupBuilder { get; }
    }
}