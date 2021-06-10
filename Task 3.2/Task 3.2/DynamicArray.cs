using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace Task_3._2
{
    class DynamicArray<T> : ICloneable, IEnumerable<T>, IEnumerable
    {
        private T[] _items;

        private int _size;

        private static readonly T[] _emptyArray = new T[8];


        public DynamicArray()
        {
            _items = _emptyArray;
            Capacity = 8;
        }

        public DynamicArray(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();

            if (capacity == 0)
            {
                _items = _emptyArray;
                Capacity = 8;
            }

            if (capacity > 0)
            {
                _items = new T[capacity];
                Capacity = (int)Math.Pow(8, Math.Pow((int)Math.Log2(capacity) / 1, 1/3)); // ((log_2(capacity) ^ 1/3) == log_8(capacity)
            }
            
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            else
            {

                int cap = (int)Math.Pow(8, Math.Pow((int)Math.Log2(collection.Count()) / 1, 1/3));
                _items = new T[cap];
                Capacity = cap;
                _size = collection.Count();
                Array.Copy(collection.ToArray(), _items, _size);
            }
        }

        public int Length
        {
            get => _size;
        }

        public int Capacity {
            get => _items.Length;
            set
            {
                if (value < _size)
                    throw new ArgumentOutOfRangeException();
                if (value == _items.Length)
                    return;
                if (value > 0)
                {
                    T[] buff = new T[value];
                    Array.Copy(_items, buff, _items.Length);
                    _items = buff;
                }

            }

        }

        public T this[int index]
        {
            get
            {
                if (index > _size || index < -_size)
                    throw new ArgumentOutOfRangeException();

                if (index >= 0)
                    return _items[index];

                else
                    return _items[_size + index];
            }
            set
            {

                if (index > _size || index < -_size)
                    throw new ArgumentOutOfRangeException();

                if (index >= 0)
                    _items[index] = value;

                else
                    _items[_size + index] = value;

                _size++;
            }
        }

        public bool Insert(T obj, int index)
        {
            if (index > _size || index < -_size)
                throw new ArgumentOutOfRangeException();
            else if (_items.Length == _size)
                EnsureArray(_size + 1);

            if (index == 0)
            {
                if (_items.Length == _size)
                    EnsureArray(_size + 1);
                T[] buff = new T[_size + 1];
                Array.Copy(_items, 0, buff, 1, _size++);
                buff[0] = obj;
                _items = buff;
                return true;
            }

            if (index == _size)
            {
                T[] buff = new T[_size + 1];
                buff[_size] = obj;
                Array.Copy(_items, 0, buff, 0, _size++);
                _items = buff;
                return true;
            }
            else
            {
                T[] buff = new T[Capacity];
                _size++;
                Array.Copy(_items, 0, buff, 0, index);
                Array.Copy(_items, index, buff, index + 1, _size - index- 1);
                buff[index] = obj;
                _items = buff;
                return true;
            }
        }

        public int IndexOf(T obj)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].Equals(obj))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Remove(T obj)
        {
            int indx = IndexOf(obj);
            if (indx == _size - 1)
            {
                T[] buff = new T[_size - 1];
                Array.Copy(_items, 0, buff, 0, --_size);
                _items = buff;
                return true;
            }
            if (indx == 0)
            {
                T[] buff = new T[_size - 1];
                Array.Copy(_items, 1, buff, 0, --_size);
                _items = buff;
                return true; 
            }
            if (indx > 0)
            {
                T[] buff = new T[_size - 1];
                Array.Copy(_items, 0, buff, 0, indx + 1);
                Array.Copy(_items, indx + 1, buff, indx, _size - 1 - indx - 1);
                _size--;
                _items = buff;
                return true;
            }
            return false;
        }

        public void AddRange(IEnumerable<T> elements)
        {
            EnsureArray(_size + elements.Count());
            foreach(var item in elements)
            {
                this.Add(item);
            }
        }

        public void Add(T elem)
        {
            if (_items.Length == _size)
                EnsureArray(_size + 1);
            _items[_size++] = elem;

        }

        private bool EnsureArray(int newSize)
        {
            if (newSize >= Capacity)
            {
                T[] buff;

                if (newSize < 2 * Capacity)
                    buff = new T[_items.Length * 2];
                else
                {
                    buff = new T[(int)Math.Pow(8, Math.Pow((int)Math.Log2(newSize) / 1, 1/3) + 1)];
                }

                for (int i = 0; i < _items.Length; i++)
                    buff[i] = _items[i];

                _items = buff;
                Capacity = _items.Length;
                return true;
            }
            return false;
        }

        public T[] ToArray()
        {
            T[] buff = new T[_size];
            Array.Copy(_items, buff, _size);
            return buff;
        }

        public object Clone() => new DynamicArray<T>(_items);

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();


    }
}
