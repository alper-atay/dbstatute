using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Delete : Statute, IDelete
    {
        public abstract int DeletedCount { get; }
    }
}