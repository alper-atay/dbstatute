using System.Collections.Generic;

namespace DbStatute.Interfaces.Queries
{
    public interface IIdentifiablesQuery
    {
        IEnumerable<object> Ids { get; }
    }
}
