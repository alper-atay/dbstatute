namespace DbStatute.Sample.Models
{
    public class SingleUserSelectByQuery : SingleSelectByQuery<int, User, UserSelectQuery>
    {
        public SingleUserSelectByQuery(UserSelectQuery selectQuery) : base(selectQuery)
        {
        }

        protected override void OnFailed()
        {
        }

        protected override void OnSucceed()
        {
        }
    }

}
