namespace DbStatute
{
    public abstract class Delete : Statute
    {
        public abstract int DeletedCount { get; }
    }
}