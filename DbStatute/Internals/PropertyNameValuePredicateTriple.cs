using Basiclog;

namespace DbStatute.Internals
{
    internal readonly struct PropertyNamePredicatePair
    {

    }


    internal readonly struct PropertyNameValuePredicateTriple
    {
        public PropertyNameValuePredicateTriple(string name, object value, ReadOnlyLogbookPredicate<object> predicate)
        {
            Name = name;
            Value = value;
            Predicate = predicate;
        }

        public string Name { get; }
        public ReadOnlyLogbookPredicate<object> Predicate { get; }
        public object Value { get; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}