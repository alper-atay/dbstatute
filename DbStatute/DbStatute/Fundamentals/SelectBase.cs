using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Fundamentals
{
    public abstract class SelectBase : StatuteBase, ISelectBase
    {
        public abstract int SelectedCount { get; }

        public abstract int? MaxSelectCount { get; }
    }

    public abstract class SelectBase<TModel> : StatuteBase<TModel>, ISelectBase<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int SelectedCount { get; }

        public abstract int? MaxSelectCount { get; }
    }
}