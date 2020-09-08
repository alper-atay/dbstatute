using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface IIdentifiablesQuery
    {
        IEnumerable<object> Id { get; }
    }
}
