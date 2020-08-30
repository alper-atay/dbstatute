using Basiclog;
using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;

namespace DbStatute.Querying.Builders
{
    public class MergeQueryGroupBuilder : QueryGroupBuilder, IMergeQueryGroupBuilder
    {
        public MergeQueryGroupBuilder() : base(QueryGroupUsage.Merge)
        {
        }

        public MergeQueryGroupBuilder(IFieldBuilder fieldBuilder) : base(QueryGroupUsage.Merge, fieldBuilder)
        {
        }

        public MergeQueryGroupBuilder(IFieldBuilder fieldBuilder, IValueFieldQualifier valueFieldQualifier, IPredicateFieldQualifier predicateFieldQualifier) : base(QueryGroupUsage.Merge, fieldBuilder, valueFieldQualifier, predicateFieldQualifier)
        {
        }

        public override IReadOnlyLogbook Build(out QueryGroup queryGroup)
        {
            throw new NotImplementedException();
        }
    }

    public class MergeQueryGroupBuilder<TModel> : QueryGroupBuilder<TModel>, IMergeQueryGroupBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public MergeQueryGroupBuilder() : base(QueryGroupUsage.Merge)
        {
        }

        public MergeQueryGroupBuilder(IFieldBuilder<TModel> fieldBuilder) : base(QueryGroupUsage.Merge, fieldBuilder)
        {
        }

        public MergeQueryGroupBuilder(IFieldBuilder<TModel> fieldBuilder, IValueFieldQualifier<TModel> valueFieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier) : base(QueryGroupUsage.Merge, fieldBuilder, valueFieldQualifier, predicateFieldQualifier)
        {
        }

        public override IReadOnlyLogbook Build(out QueryGroup queryGroup)
        {
            throw new NotImplementedException();
        }
    }
}
