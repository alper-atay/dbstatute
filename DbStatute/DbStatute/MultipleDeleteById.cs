using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class MultipleDeleteById<TModel, TMultipleSelectById> : MultipleDelete<TModel, TMultipleSelectById>, IMultipleDeleteById<TModel, TMultipleSelectById>

        where TModel : class, IModel, new()
        where TMultipleSelectById : IMultipleSelectById<TModel>
    {
        protected MultipleDeleteById(TMultipleSelectById multipleSelectById) : base(multipleSelectById)
        {
        }

        public TMultipleSelectById MultipleSelectById => MultipleSelect;
    }
}