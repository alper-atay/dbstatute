using Basiclog;
using RepoDb.Interfaces;
using System;
using System.Data;

namespace DbStatute.Interfaces
{
    public interface IStatute
    {
        event Action Failed;

        event Action Succeed;

        public IReadOnlyLogbook ReadOnlyLogs { get; }
        int? CommandTimeout { get; set; }
        string Hints { get; set; }
        IStatementBuilder StatementBuilder { get; set; }
        StatuteResult StatuteResult { get; set; }
        ITrace Trace { get; set; }
        IDbTransaction Transaction { get; set; }
    }
}