using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Insert : Statute, IInsert
    {
        public abstract int InsertedCount { get; }
    }
}