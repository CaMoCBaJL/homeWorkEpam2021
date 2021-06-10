using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task_3._2
{
    //TODO доделать класс и погонять таску
    class CycledDynamicArray<T> : IEnumerable, IEnumerable<T>
    {
        private DynamicArray<T> _arr;


        public CycledDynamicArray() => _arr = new DynamicArray<T>();


        public CycledDynamicArray(int capacity) => _arr = new DynamicArray<T>(capacity);


        public CycledDynamicArray(IEnumerable<T> elements) => _arr = new DynamicArray<T>(elements);
        

        public void Add(T elem)
        {
            _arr.Add(elem);
        }

        public int Length
        {
            get => _arr.Length;
        }

        public T this[int index]
        { 
            get => _arr[index];
            set => _arr[index] = value;
        }

        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private CycledDynamicArray<T> arr;

            private int index;

            private T current;

            public Enumerator(CycledDynamicArray<T> array)
            {
                arr = array;
                index = 0;
                current = default;
            }

            public T Current => current;

            object IEnumerator.Current
            {
                get
                {
                    if (index > arr.Length)
                        throw new InvalidOperationException();
                    return (object)current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (index == arr.Length)
                    Reset();
                current = arr[index];
                index++;
                return true;
            }

            public void Reset()
            {
                index = 0;
                current = default;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)new Enumerator(this);


    }
}

