namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IStatuteQuery
    {
    }

    public interface IStatuteQuery<TModel> : IStatuteQuery
        where TModel : class, IModel, new()
    {
    }
}