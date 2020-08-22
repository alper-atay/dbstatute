namespace DbStatute
{
    public abstract class Delete : Statute
    {
        public int DeletedCount { get; protected set; }
    }
}