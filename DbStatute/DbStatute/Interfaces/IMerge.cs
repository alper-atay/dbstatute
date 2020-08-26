namespace DbStatute.Interfaces
{
    public interface IMerge : IStatute
    {
        int MergedCount { get; }
    }

    public interface IMerge<TModel> : IStatute<TModel>, IMerge
        where TModel : class, IModel, new()
    {
    }
}