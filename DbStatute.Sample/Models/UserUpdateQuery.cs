using Basiclog;
using DbStatute.Querying;

namespace DbStatute.Sample.Models
{
    public class UserUpdateQuery : UpdateQuery<int, User>
    {
        public void SetFullNameUpdateField(string fullName)
        {
            SetUpdateField(x => x.FullName, fullName, FullNameExaminer);
        }

        public void SetNickUpdateField(string nick)
        {
            SetUpdateField(x => x.Nick, nick, NickExaminer);
        }

        public void UnsetFullNameUpdateField()
        {
            UnsetUpdateField(x => x.FullName);
        }

        public void UnsetNickUpdateField()
        {
            UnsetUpdateField(x => x.Nick);
        }

        // Empty examiners

        private static IReadOnlyLogbook FullNameExaminer(string fullName)
        {
            return Logger.New();
        }

        private static IReadOnlyLogbook NickExaminer(string nick)
        {
            return Logger.New();
        }
    }
}