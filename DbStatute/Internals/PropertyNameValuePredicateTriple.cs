using Basiclog;

namespace DbStatute.Internals
{
    internal readonly struct PropertyNameValuePredicateTriple<TValue>
    {
        public PropertyNameValuePredicateTriple(string name, TValue value, ReadOnlyLogbookPredicate<TValue> predicate)
        {
            Name = name;
            Value = value;
            Predicate = predicate;
        }

        public string Name { get; }
        public ReadOnlyLogbookPredicate<TValue> Predicate { get; }
        public TValue Value { get; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}