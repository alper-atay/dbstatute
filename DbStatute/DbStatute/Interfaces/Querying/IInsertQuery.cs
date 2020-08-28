using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Fundamentals;

namespace DbStatute.Interfaces.Querying
{
    public interface IInsertQuery : IStatuteQueryBase
    {
        IModelBuilder ModelQueryQualifier { get; }
    }

    public interface IInsertQuery<TModel> : IStatuteQueryBase<TModel>, IInsertQuery
        where TModel : class, IModel, new()
    {
        new IModelBuilder<TModel> ModelQueryQualifier { get; }
    }
}