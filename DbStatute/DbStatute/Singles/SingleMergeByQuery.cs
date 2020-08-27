using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute
{
    public abstract class SingleMergeByQuery<TModel, TMergeQuery> : SingleMerge<TModel>
        where TModel : class, IModel, new()
        where TMergeQuery : IMergeQuery<TModel>
    {
        protected SingleMergeByQuery() : base()
        {

        }

        protected SingleMergeByQuery(IModelQueryQualifier<TModel> modelQueryQualifier) : base(modelQueryQualifier)
        {
        }
    }
}