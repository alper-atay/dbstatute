using Basiclog;
using DbStatute.Interfaces.Querying.Builders;
using System;

namespace DbStatute.Querying.Builders
{
    public abstract class Builder : IBuilder
    {
        private object _built;

        public object Built => _built;

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