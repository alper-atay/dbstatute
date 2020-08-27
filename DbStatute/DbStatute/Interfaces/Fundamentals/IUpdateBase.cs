namespace DbStatute.Interfaces.Fundamentals
{
    public interface IUpdateBase : IStatuteBase
    {
        int UpdatedCount { get; }
    }

    public interface IUpdateBase<TModel> : IStatuteBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}