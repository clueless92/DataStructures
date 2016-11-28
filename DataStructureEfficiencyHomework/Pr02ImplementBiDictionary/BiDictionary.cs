namespace Pr02ImplementBiDictionary
{
    using System;
    using System.Collections.Generic;

    public class BiDictionary<K1, K2, V>
    {
        private Dictionary<K1, List<V>> valuesByFirstKey;
        private Dictionary<K2, List<V>> valuesBySecondKey;
        private Dictionary<Tuple<K1, K2>, List<V>> valuesByBothKeys;

        public BiDictionary()
        {
            this.valuesByFirstKey = new Dictionary<K1, List<V>>();
            this.valuesBySecondKey = new Dictionary<K2, List<V>>();
            this.valuesByBothKeys = new Dictionary<Tuple<K1, K2>, List<V>>();
        }

        public void Add(K1 key1, K2 key2, V value)
        {
            if (!this.valuesByFirstKey.ContainsKey(key1))
            {
                this.valuesByFirstKey.Add(key1, new List<V>());
            }

            this.valuesByFirstKey[key1].Add(value);

            if (!this.valuesBySecondKey.ContainsKey(key2))
            {
                this.valuesBySecondKey.Add(key2, new List<V>());
            }

            this.valuesBySecondKey[key2].Add(value);

            Tuple<K1, K2> keyTuple = new Tuple<K1, K2>(key1, key2);
            if (!this.valuesByBothKeys.ContainsKey(keyTuple))
            {
                this.valuesByBothKeys.Add(keyTuple, new List<V>());
            }

            this.valuesByBothKeys[keyTuple].Add(value);
        }

        public IEnumerable<V> Find(K1 key1, K2 key2)
        {
            Tuple<K1, K2> keyTuple = new Tuple<K1, K2>(key1, key2);
            if (!this.valuesByBothKeys.ContainsKey(keyTuple))
            {
                return new List<V>();
            }

            return this.valuesByBothKeys[keyTuple];
        }

        public IEnumerable<V> FindByKey1(K1 key1)
        {
            if (!this.valuesByFirstKey.ContainsKey(key1))
            {
                return new List<V>();
            }

            return this.valuesByFirstKey[key1];
        }

        public IEnumerable<V> FindByKey2(K2 key2)
        {
            if (!this.valuesBySecondKey.ContainsKey(key2))
            {
                return new List<V>();
            }

            return this.valuesBySecondKey[key2];
        }

        public bool Remove(K1 key1, K2 key2)
        {
            Tuple<K1, K2> keyTuple = new Tuple<K1, K2>(key1, key2);
            if (!this.valuesByBothKeys.ContainsKey(keyTuple))
            {
                return false;
            }

            this.valuesByBothKeys.Remove(keyTuple);
            this.valuesByFirstKey.Remove(key1);
            this.valuesBySecondKey.Remove(key2);

            return true;
        }
    }
}
