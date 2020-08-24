using Basiclog;
using DbStatute.Querying;

namespace DbStatute.Sample.Models
{
    public class UserUpdateQuery : UpdateQuery<int, User>
    {
        public UserUpdateQuery()
        {
            RegisterPredicate(x => x.Nick, NickExaminer);
            RegisterPredicate(x => x.FullName, FullNameExaminer);
        }

        // Empty examiners

        private static IReadOnlyLogbook FullNameExaminer(object fullName)
        {
            return Logger.New();
        }

        private static IReadOnlyLogbook NickExaminer(object nick)
        {
            return Logger.New();
        }
    }
}