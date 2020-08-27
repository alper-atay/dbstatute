using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Fundamentals
{
    public abstract class MergeBase : StatuteBase, IMergeBase
    {
        public abstract int MergedCount { get; }
    }

    public abstract class MergeBase<TModel> : StatuteBase<TModel>, IMergeBase<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int MergedCount { get; }
    }
}