using DbStatute.Interfaces;
using System;

namespace DbStatute
{
    public abstract class SingleDeleteById<TId, TModel, TSingleSelectById> : SingleDelete<TId, TModel, TSingleSelectById>
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
        where TSingleSelectById : SingleSelectById<TId, TModel>
    {
        protected SingleDeleteById(TSingleSelectById singleSelectById) : base(singleSelectById)
        {
        }
    }
}