namespace Pr01DictionaryImplementation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class LinkedHashMap<K, V> : IEnumerable<Entry<K, V>> where K : IComparable
    {
        private const int InitialCapacity = 16;
        private const float LoadFactor = 0.75f;

        private LinkedList<Entry<K, V>>[] slots; 

        public int Count { get; private set; }

        public int Capacity
        {
            get { return this.slots.Length; }
        }

        public LinkedHashMap(int capacity = InitialCapacity)
        {
            this.slots = new LinkedList<Entry<K, V>>[capacity];
            this.Count = 0;
        }

        public void Add(K key, V value)
        {
            this.GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<Entry<K, V>>();
            }

            foreach (var element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException("Key already exists: " + key);
                }
            }

            var newElement = new Entry<K, V>(key, value);
            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
        }

        private int FindSlotNumber(K key)
        {
            var slotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;
            return slotNumber;
        }

        private void GrowIfNeeded()
        {
            if((float)(this.Count + 1) / this.Capacity > LoadFactor)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newHashTable = new LinkedHashMap<K, V>(2 * this.slots.Length);
            foreach (var element in this)
            {
                newHashTable.Add(element.Key, element.Value);
            }

            this.slots = newHashTable.slots;
            this.Count = newHashTable.Count;
        }

        public bool AddOrReplace(K key, V value)
        {
            this.GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<Entry<K, V>>();
            }

            foreach (var element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    element.Value = value;
                    return true;
                }
            }

            var newElement = new Entry<K, V>(key, value);
            this.slots[slotNumber].AddLast(newElement);
            this.Count++;

            return false;
        }

        public V Get(K key)
        {
            var element = this.Find(key);
            if (element == null)
            {
                throw new KeyNotFoundException();
            }

            return element.Value;
        }

        public V this[K key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(K key, out V value)
        {
            var element = this.Find(key);
            if (element == null)
            {
                value = default(V);
                return false;
            }

            value = element.Value;
            return true;
        }

        public Entry<K, V> Find(K key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];
            if (elements != null)
            {
                foreach (var element in elements)
                {
                    if (element.Key.Equals(key))
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(K key)
        {
            var element = this.Find(key);
            return element != null; 
        }

        public bool Remove(K key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];
            if (elements != null)
            {
                var currentElement = elements.First;
                while (currentElement != null)
                {
                    if (currentElement.Value.Key.Equals(key))
                    {
                        elements.Remove(currentElement);
                        this.Count--;
                        return true;
                    }

                    currentElement = currentElement.Next;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<Entry<K, V>>[this.Capacity];
            this.Count = 0;
        }

        public IEnumerable<K> Keys
        {
            get { return this.Select(element => element.Key); }
        }

        public IEnumerable<V> Values
        {
            get { return this.Select(element => element.Value); }
        }

        public IEnumerator<Entry<K, V>> GetEnumerator()
        {
            foreach (var elements in this.slots)
            {
                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        yield return element;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
