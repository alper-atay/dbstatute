namespace DbStatute.Interfaces
{
    public interface IMultipleMerge<TModel> : IMerge
        where TModel : class, IModel, new()
    {
    }
}