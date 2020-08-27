using System.Collections.Generic;

namespace DbStatute.Interfaces
{
    public interface IIds
    {
        IEnumerable<object> Ids { get; }
    }
}