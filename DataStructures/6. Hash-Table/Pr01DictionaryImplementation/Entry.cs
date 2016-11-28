namespace Pr01DictionaryImplementation
{
    using System;
    public class Entry<K, V> where K : IComparable
    {
        public Entry(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public K Key { get; set; }
        public V Value { get; set; }

        public override bool Equals(object other)
        {
            var element = (Entry<K, V>) other;
            var equals = Equals(this.Key, element.Key) && Equals(this.Value, element.Value);
            return equals;
        }

        public override int GetHashCode()
        {
            return this.CombineHashCodes(this.Key.GetHashCode(), this.Value.GetHashCode());
        }

        private int CombineHashCodes(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }

        public override string ToString()
        {
            return string.Format(" [{0} -> {1}]", this.Key, this.Value);
        }
    }
}