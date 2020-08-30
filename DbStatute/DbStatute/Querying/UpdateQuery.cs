using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class UpdateQuery : StatuteQueryBase, IUpdateQuery
    {
        public UpdateQuery(IMergeQueryGroupBuilder mergeQueryGroupBuilder)
        {
            MergeQueryGroupBuilder = mergeQueryGroupBuilder ?? throw new ArgumentNullException(nameof(mergeQueryGroupBuilder));
        }

        public UpdateQuery()
        {
            MergeQueryGroupBuilder = new MergeQueryGroupBuilder();
        }

        public IMergeQueryGroupBuilder MergeQueryGroupBuilder { get; }
    }

    public class UpdateQuery<TModel> : StatuteQueryBase<TModel>, IUpdateQuery<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateQuery()
        {
            MergeQueryGroupBuilder = new MergeQueryGroupBuilder<TModel>();
        }

        public UpdateQuery(IMergeQueryGroupBuilder<TModel> mergeQueryGroupBuilder)
        {
            MergeQueryGroupBuilder = mergeQueryGroupBuilder ?? throw new ArgumentNullException(nameof(mergeQueryGroupBuilder));
        }

        public IMergeQueryGroupBuilder<TModel> MergeQueryGroupBuilder { get; }
        IMergeQueryGroupBuilder IUpdateQuery.MergeQueryGroupBuilder => throw new NotImplementedException();
    }
}