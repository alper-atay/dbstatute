namespace DbStatute.Interfaces.Proxies.Inserts
{
    public interface IInsertProxyWithSourceModel : IInsertProxy, ISourceModel
    {
    }

    public interface IInsertProxyWithSourceModel<TModel> : IInsertProxy<TModel>, ISourceModel<TModel>
        where TModel : class, IModel, new()
    {
    }
}