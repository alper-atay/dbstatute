using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateByProxyWithWhereQuery : IMultipleUpdateByProxy
    {
        IWhereQuery WhereQuery { get; }
    }

    public interface IMultipleUpdateByProxyWithWhereQuery<TModel> : IMultipleUpdateByProxy<TModel>, IMultipleUpdateByProxyWithWhereQuery
        where TModel : class, IModel, new()
    {
        new IWhereQuery<TModel> WhereQuery { get; }
    }
}