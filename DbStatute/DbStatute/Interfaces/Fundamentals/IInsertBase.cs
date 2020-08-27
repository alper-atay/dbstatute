namespace DbStatute.Interfaces.Fundamentals
{
    public interface IInsertBase : IStatuteBase
    {
        int InsertedCount { get; }
    }

    public interface IInsertBase<TModel> : IStatuteBase<TModel>, IInsertBase
        where TModel : class, IModel, new()
    {
    }
}