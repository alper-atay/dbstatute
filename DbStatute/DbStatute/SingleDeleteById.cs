using DbStatute.Interfaces;
using System;

namespace DbStatute
{
    public abstract class SingleDeleteById<TId, TModel, TSingleSelectById> : SingleDelete<TId, TModel, TSingleSelectById>, ISingleDeleteById<TId, TModel, TSingleSelectById>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSingleSelectById : SingleSelectById<TId, TModel>
    {
        protected SingleDeleteById(TSingleSelectById singleSelectById) : base(singleSelectById)
        {
        }

        public TSingleSelectById SingleSelectById => SingleSelect;
    }
}