using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Merge : Statute, IMerge
    {
        public abstract int MergedCount { get; }
    }
}