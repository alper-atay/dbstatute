using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Select : Statute, ISelect
    {
        public abstract int SelectedCount { get; }
    }
}