namespace DbStatute.Interfaces.Proxies.Inserts
{
    public interface IInsertProxyWithSourceModels : IInsertProxy, ISourceModels
    {
    }

    public interface IInsertProxyWithSourceModels<TModel> : IInsertProxy<TModel>, ISourceModels<TModel>
        where TModel : class, IModel, new()
    {
    }
}