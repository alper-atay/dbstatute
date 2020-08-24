using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Update : Statute, IUpdate
    {
        public abstract int UpdatedCount { get; }
    }
}