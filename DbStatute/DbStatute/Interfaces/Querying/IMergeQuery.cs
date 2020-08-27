using DbStatute.Interfaces.Querying.Fundamentals;
using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Querying
{
    public interface IMergeQuery : IStatuteQueryBase
    {
        IModelQueryQualifier ModelQueryQualifier { get; }
    }

    public interface IMergeQuery<TModel> : IStatuteQueryBase<TModel>, IMergeQuery
        where TModel : class, IModel, new()
    {
        new IModelQueryQualifier<TModel> ModelQueryQualifier { get; }
    }
}