using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Insert : Statute, IInsert
    {
        public abstract int InsertedCount { get; }
    }

    public abstract class Insert<TModel> : Statute<TModel>, IInsert<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int InsertedCount { get; }
    }
}