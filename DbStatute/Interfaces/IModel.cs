using System;

namespace DbStatute.Interfaces
{
    public interface IModel<TId> : IIdentifiable<TId>
        where TId : struct, IConvertible
    {
    }
}