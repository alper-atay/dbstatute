using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Select : Statute, ISelect
    {
        public ICacheable Cacheable { get; set; }
        public abstract int SelectedCount { get; }
    }

    public abstract class Select<TModel> : Statute<TModel>, ISelect<TModel>
        where TModel : class, IModel, new()
    {
        public ICacheable Cacheable { get; set; }
        public abstract int SelectedCount { get; }
    }
}