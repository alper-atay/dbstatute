using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class MergeQuery : StatuteQueryBase, IMergeQuery
    {
        public MergeQuery()
        {
            MergeQueryGroupBuilder = new MergeQueryGroupBuilder();
        }

        public MergeQuery(IMergeQueryGroupBuilder mergeQueryGroupBuilder)
        {
            MergeQueryGroupBuilder = mergeQueryGroupBuilder ?? throw new ArgumentNullException(nameof(mergeQueryGroupBuilder));
        }

        public IMergeQueryGroupBuilder MergeQueryGroupBuilder { get; }
    }

    public class MergeQuery<TModel> : StatuteQueryBase<TModel>, IMergeQuery<TModel>
        where TModel : class, IModel, new()
    {
        public MergeQuery()
        {
            MergeQueryGroupBuilder = new MergeQueryGroupBuilder<TModel>();
        }

        public MergeQuery(IMergeQueryGroupBuilder<TModel> mergeQueryGroupBuilder)
        {
            MergeQueryGroupBuilder = mergeQueryGroupBuilder ?? throw new ArgumentNullException(nameof(mergeQueryGroupBuilder));
        }

        public IMergeQueryGroupBuilder<TModel> MergeQueryGroupBuilder { get; }
        IMergeQueryGroupBuilder IMergeQuery.MergeQueryGroupBuilder => MergeQueryGroupBuilder;
    }
}