using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Fundamentals
{
    public abstract class UpdateBase : StatuteBase, IUpdateBase
    {
        public abstract int UpdatedCount { get; }
    }

    public abstract class UpdateBase<TModel> : StatuteBase<TModel>, IUpdateBase<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int UpdatedCount { get; }
    }
}