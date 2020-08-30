using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class InsertQuery : IInsertQuery
    {
        public InsertQuery()
        {
            MergeQueryGroupBuilder = new MergeQueryGroupBuilder();
        }

        public InsertQuery(IMergeQueryGroupBuilder mergeQueryGroupBuilder)
        {
            MergeQueryGroupBuilder = mergeQueryGroupBuilder ?? throw new ArgumentNullException(nameof(mergeQueryGroupBuilder));
        }

        public IMergeQueryGroupBuilder MergeQueryGroupBuilder { get; }
    }

    public class InsertQuery<TModel> : IInsertQuery<TModel>
        where TModel : class, IModel, new()
    {
        public InsertQuery()
        {
            MergeQueryGroupBuilder = new MergeQueryGroupBuilder<TModel>();
        }

        public InsertQuery(IMergeQueryGroupBuilder<TModel> mergeQueryGroupBuilder)
        {
            MergeQueryGroupBuilder = mergeQueryGroupBuilder ?? throw new ArgumentNullException(nameof(mergeQueryGroupBuilder));
        }

        public IMergeQueryGroupBuilder<TModel> MergeQueryGroupBuilder { get; }

        IMergeQueryGroupBuilder IInsertQuery.MergeQueryGroupBuilder => MergeQueryGroupBuilder;
    }
}