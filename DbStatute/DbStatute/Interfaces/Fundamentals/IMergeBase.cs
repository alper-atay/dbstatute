namespace DbStatute.Interfaces.Fundamentals
{
    public interface IMergeBase : IStatuteBase
    {
        int MergedCount { get; }
    }

    public interface IMergeBase<TModel> : IStatuteBase<TModel>, IMergeBase
        where TModel : class, IModel, new()
    {
    }
}