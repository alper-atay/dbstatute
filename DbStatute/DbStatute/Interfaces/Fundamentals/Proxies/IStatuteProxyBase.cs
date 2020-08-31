namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IStatuteProxyBase
    {
    }

    public interface IStatuteProxyBase<TModel> : IStatuteProxyBase
        where TModel : class, IModel, new()
    {
    }
}