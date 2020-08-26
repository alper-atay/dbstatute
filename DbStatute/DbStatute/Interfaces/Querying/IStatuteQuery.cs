namespace DbStatute.Interfaces.Querying
{
    public interface IStatuteQuery
    {
    }

    public interface IStatuteQuery<TModel> : IStatuteQuery
        where TModel : class, IModel, new()
    {
    }
}