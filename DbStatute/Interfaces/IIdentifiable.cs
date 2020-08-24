using System;

namespace DbStatute.Interfaces
{
    public interface IIdentifiable<TId>
        where TId : notnull, IConvertible
    {
        TId Id { get; set; }
    }
}