using Basiclog;
using DbStatute.Interfaces;
using RepoDb.Interfaces;
using System;
using System.Data;

namespace DbStatute
{
    public abstract class Statute : IStatute
    {
        private StatuteResult _statuteResult = StatuteResult.Unknown;

        public Statute()
        {
            Failed += OnFailed;
            Succeed += OnSucceed;
        }

        public IReadOnlyLogbook ReadOnlyLogs => Logs;
        protected ILogbook Logs { get; } = Logger.NewLogbook();

        #region IStatute

        public event Action Failed;

        public event Action Succeed;

        public int? CommandTimeout { get; set; } = null;
        public string Hints { get; set; } = null;
        public IStatementBuilder StatementBuilder { get; set; } = null;

        public StatuteResult StatuteResult
        {
            get => _statuteResult;
            set
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
        public IDbTransaction Transaction { get; set; } = null;

        #endregion IStatute

        protected abstract void OnFailed();

        protected abstract void OnSucceed();
    }
}