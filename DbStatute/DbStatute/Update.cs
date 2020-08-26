using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Update : Statute, IUpdate
    {
        public abstract int UpdatedCount { get; }
    }

    public abstract class Update<TModel> : Statute<TModel>, IUpdate<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int UpdatedCount { get; }
    }
}