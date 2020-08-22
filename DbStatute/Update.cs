namespace DbStatute
{
    public abstract class Update : Statute
    {
        public abstract int UpdatedCount { get; }
    }
}