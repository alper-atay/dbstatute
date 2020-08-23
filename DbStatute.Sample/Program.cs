using System;
using DbStatute.Sample.Models;

namespace DbStatute.Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            UserUpdateQuery userUpdateQuery = new UserUpdateQuery();
            userUpdateQuery.IsEnableUpdateField(x => x.FullName);
            userUpdateQuery.SetUpdateField(x => x.FullName, "New full name");

            SingleUserUpdate singleUserUpdate = new SingleUserUpdate();


            Console.WriteLine("Hello World!");
        }
    }
}
