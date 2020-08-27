using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Fundamentals
{
    public abstract class SelectBase : StatuteBase, ISelectBase
    {
        public ICacheable Cacheable { get; set; }
        public abstract int SelectedCount { get; }
    }

    public abstract class SelectBase<TModel> : StatuteBase<TModel>, ISelectBase<TModel>
        where TModel : class, IModel, new()
    {
        public ICacheable Cacheable { get; set; }
        public abstract int SelectedCount { get; }
    }
}