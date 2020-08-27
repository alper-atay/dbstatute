namespace DbStatute.Interfaces
{
    public interface ISingleDeleteById<TSingleSelectById> : ISingleDelete<TSingleSelectById>
        where TSingleSelectById : ISingleSelectById
    {
        TSingleSelectById SingleSelectById { get; }
    }

    public interface ISingleDeleteById<TModel, TSingleSelectById> : ISingleDelete<TModel, TSingleSelectById>, ISingleDeleteById<TSingleSelectById>
        where TModel : class, IModel, new()
        where TSingleSelectById : ISingleSelectById<TModel>
    {
        new TSingleSelectById SingleSelectById { get; }
    }
}