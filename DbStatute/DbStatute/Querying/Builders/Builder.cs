using Basiclog;
using DbStatute.Interfaces.Querying.Builders;
using System;

namespace DbStatute.Querying.Builders
{
    public abstract class Builder : IBuilder
    {
        private object _built;

        public object Built { get; }

        public IReadOnlyLogbook ReadOnlyLogs => throw new NotImplementedException();
        protected ILogbook Logs { get; } = Logger.NewLogbook();

        public bool Build(out object built)
        {
            bool isBuilt = BuildOperation(out built);

            _built = built;

            return isBuilt;
        }

        protected abstract bool BuildOperation(out object built);
    }

    public abstract class Builder<T> : IBuilder<T>
    {
        public T Built => throw new NotImplementedException();

        public IReadOnlyLogbook ReadOnlyLogs => throw new NotImplementedException();

        object IBuilder.Built => throw new NotImplementedException();

        public bool Build(out T built)
        {
            throw new NotImplementedException();
        }

        public bool Build(out object built)
        {
            throw new NotImplementedException();
        }
    }
}