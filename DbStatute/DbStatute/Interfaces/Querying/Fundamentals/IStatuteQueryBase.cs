namespace DbStatute.Interfaces.Querying.Fundamentals
{
    public interface IStatuteQueryBase
    {
    }

    public interface IStatuteQueryBase<TModel> : IStatuteQueryBase
        where TModel : class, IModel, new()
    {
    }
}