namespace DbStatute
{
    public abstract class Insert : Statute
    {
        public abstract int InsertedCount { get; }
    }
}