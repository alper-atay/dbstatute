using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Select : Statute, ISelect
    {
        public ICacheable Cacheable { get; set; }
        public abstract int SelectedCount { get; }
    }
}