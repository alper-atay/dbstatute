using Basiclog;
using DbStatute.Interfaces.Builders;

namespace DbStatute.Builders
{
    public abstract class Builder : IBuilder
    {
        public abstract object Built { get; }

        public IReadOnlyLogbook ReadOnlyLogs => Logs;

        protected ILogbook Logs { get; } = Logger.NewLogbook();
    }

    public abstract class Builder<T> : IBuilder<T>
    {
        private T _built;

        public T Built => _built;

        object IBuilder.Built => Built;

        public IReadOnlyLogbook ReadOnlyLogs => Logs;

        protected ILogbook Logs { get; } = Logger.NewLogbook();

        public bool Build(out T built)
        {
            bool isBuilt = BuildOperation(out built);

            _built = built;

            return isBuilt;
        }

        protected abstract bool BuildOperation(out T built);
    }
}