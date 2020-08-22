using DbStatute.Interfaces;
using System;

namespace DbStatute
{
    public abstract class MultipleDeleteById<TId, TModel, TMultipleSelectById> : MultipleDelete<TId, TModel, TMultipleSelectById>
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
        where TMultipleSelectById : MultipleSelectById<TId, TModel>
    {
        protected MultipleDeleteById(TMultipleSelectById multipleSelectById) : base(multipleSelectById)
        {
        }

        public TMultipleSelectById MultipleSelectById => MultipleSelect;
    }
}