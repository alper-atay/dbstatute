using DbStatute.Interfaces;

namespace DbStatute.Sample.Models
{
    public interface IUser : IModel<int>
    {
        string Nick { get; set; }
        string FullName { get; set; }
    }
}
