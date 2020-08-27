using DbStatute.Interfaces;

namespace DbStatute
{
    public abstract class SingleDeleteById<TModel, TSingleSelectById> : SingleDelete<TModel, TSingleSelectById>, ISingleDeleteById<TModel, TSingleSelectById>

        where TModel : class, IModel, new()
        where TSingleSelectById : ISingleSelectById<TModel>
    {
        protected SingleDeleteById(TSingleSelectById singleSelectById) : base(singleSelectById)
        {
        }

        public TSingleSelectById SingleSelectById => SingleSelect;
    }
}