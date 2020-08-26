using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class Delete : Statute, IDelete
    {
        public abstract int DeletedCount { get; }
    }

    public abstract class Delete<TModel> : Statute<TModel>, IDelete<TModel>
        where TModel : class, IModel, new()
    {
        public abstract int DeletedCount { get; }
    }
}