using Basiclog;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IBuilder
    {
        object Built { get; }

        IReadOnlyLogbook ReadOnlyLogs { get; }

        bool Build(out object built);
    }

    public interface IBuilder<T> : IBuilder
    {
        new T Built { get; }

        bool Build(out T built);
    }
}