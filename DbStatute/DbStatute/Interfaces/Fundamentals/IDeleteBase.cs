namespace DbStatute.Interfaces.Fundamentals
{
    public interface IDeleteBase : IStatuteBase
    {
        int DeletedCount { get; }
    }

    public interface IDeleteBase<TModel> : IStatuteBase<TModel>, IDeleteBase
        where TModel : class, IModel, new()
    {
    }
}