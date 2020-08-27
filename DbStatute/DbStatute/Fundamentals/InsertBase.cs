using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Fundamentals
{
    public abstract class InsertBase : StatuteBase, IInsertBase
    {
        public abstract int InsertedCount { get; }
    }

    public abstract class InsertBase<TModel> : StatuteBase<TModel>, IInsertBase<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int InsertedCount { get; }
    }
}