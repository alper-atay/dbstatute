using Basiclog;
using DbStatute.Interfaces;
using RepoDb.Interfaces;
using System;
using System.Data;

namespace DbStatute
{
    public abstract class Statute
    {
        private StatuteResult _statuteResult = StatuteResult.Unknown;

        public Statute()
        {
            Failed += OnFailed;
            Succeed += OnSucceed;
        }

        public event Action Failed;

        public event Action Succeed;

        public int? CommandTimeout { get; set; } = null;
        public IDbTransaction DbTransaction { get; set; } = null;
        public string Hints { get; set; } = null;
        public IReadOnlyLogbook ReadOnlyLogs => Logs;
        public IStatementBuilder StatementBuilder { get; set; } = null;

        public StatuteResult StatuteResult
        {
            get => _statuteResult;
            protected set
            {
                _statuteResult = value;

                switch (_statuteResult)
                {
                    case StatuteResult.Failure:
                        {
                            Failed?.Invoke();
                            break;
                        }
                    case StatuteResult.Success:
                        {
                            Succeed?.Invoke();
                            break;
                        }
                    case StatuteResult.Unknown:
                        {
                            break;
                        }
                }
            }
        }

        public ITrace Trace { get; set; } = null;
        protected ILogbook Logs { get; } = Logger.New();

        protected abstract void OnFailed();

        protected abstract void OnSucceed();
    }
}