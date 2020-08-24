namespace DbStatute.Sample.Models
{
    public class SingleUserSelectById : SingleSelectById<int, User>
    {
        public SingleUserSelectById(int id) : base(id)
        {
        }

        protected override void OnFailed()
        {
            Logs.Failure($"User select failed by ID:{Id}");
        }

        protected override void OnSucceed()
        {
            Logs.Success($"User selected by ID:{Id}");
        }
    }

}
