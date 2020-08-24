using System;

namespace DbStatute.Interfaces
{
    public interface IModel<TId> : IIdentifiable<TId>, ICloneable
        where TId : notnull, IConvertible
    {
    }
}