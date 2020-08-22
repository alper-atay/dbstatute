using System;

namespace DbStatute.Interfaces
{
    public interface IIdentifiable<TId> : IEquatable<IIdentifiable<TId>>
        where TId : struct, IConvertible
    {
        TId Id { get; set; }
    }
}