namespace DbStatute.Sample.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public string FullName { get; set; }
    }
}