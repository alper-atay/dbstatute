using Basiclog;
using System;

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

        public IReadOnlyLogbook ReadOnlyLogs => Logs;

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

        protected ILogbook Logs { get; } = Logger.New();

        protected abstract void OnFailed();

        protected abstract void OnSucceed();
    }
}