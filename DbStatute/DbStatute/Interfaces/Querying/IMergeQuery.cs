using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Fundamentals;

namespace DbStatute.Interfaces.Querying
{
    public interface IMergeQuery : IStatuteQueryBase
    {
        IMergeQueryGroupBuilder MergeQueryGroupBuilder { get; }
    }

    public interface IMergeQuery<TModel> : IStatuteQueryBase<TModel>, IMergeQuery
        where TModel : class, IModel, new()
    {
        new IMergeQueryGroupBuilder<TModel> MergeQueryGroupBuilder { get; }
    }
}