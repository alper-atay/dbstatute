using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Fundamentals
{
    public abstract class DeleteBase : StatuteBase, IDeleteBase
    {
        public abstract int DeletedCount { get; }
    }

    public abstract class DeleteBase<TModel> : StatuteBase<TModel>, IDeleteBase<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int DeletedCount { get; }
    }
}