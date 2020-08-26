using System;

namespace DbStatute.Interfaces
{
    public interface IModel : ICloneable
    {
        object Id { get; set; }
    }
}