using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Merge : Statute, IMerge
    {
        public abstract int MergedCount { get; }
    }

    public abstract class Merge<TModel> : Statute<TModel>, IMerge
        where TModel : class, IModel, new()
    {
        public abstract int MergedCount { get; }
    }
}