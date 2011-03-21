﻿using System;
using System.Collections.Generic;

namespace BEPUphysics.DataStructures
{
    ///<summary>
    /// No-frills list that wraps an accessible array.
    ///</summary>
    ///<typeparam name="T">Type of elements contained by the list.</typeparam>
    public class RawList<T> : IList<T>
    {
        ///<summary>
        /// Direct access to the elements owned by the raw list.
        /// Be careful about the operations performed on this list;
        /// use the normal access methods if in doubt.
        ///</summary>
        public T[] Elements;
        internal int count;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get
            {
                return count;
            }
        }

        ///<summary>
        /// Constructs an empty list.
        ///</summary>
        public RawList()
        {
            Elements = new T[4];
        }
        ///<summary>
        /// Constructs an empty list.
        ///</summary>
        ///<param name="initialCapacity">Initial capacity to allocate for the list.</param>
        ///<exception cref="ArgumentException">Thrown when the initial capacity is zero or negative.</exception>
        public RawList(int initialCapacity)
        {
            if (initialCapacity <= 0)
                throw new ArgumentException("Initial capacity must be positive.");
            Elements = new T[initialCapacity];
        }

        ///<summary>
        /// Constructs a raw list from another list.
        ///</summary>
        ///<param name="elements">List to copy.</param>
        public RawList(IList<T> elements)
            : this(elements.Count)
        {
            elements.CopyTo(Elements, 0);
            count = elements.Count;
        }


        public void RemoveAt(int index)
        {
            if (index >= count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            count--;
            if (index < count)
            {
                Elements[index] = Elements[count];
            }
            Elements[count] = default(T);

        }

        ///<summary>
        /// Gets or sets the current size allocated for the list.
        ///</summary>
        public int Capacity
        {
            get
            {
                return Elements.Length;
            }
            set
            {
                T[] newArray = new T[value];
                Array.Copy(Elements, newArray, count);
                Elements = newArray;
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public void Add(T item)
        {
            if (count == Elements.Length)
            {
                Capacity = Elements.Length * 2;
            }
            Elements[count++] = item;

        }

        ///<summary>
        /// Adds a range of elements to the list from another list.
        ///</summary>
        ///<param name="items">Elements to add.</param>
        public void AddRange(RawList<T> items)
        {
            int neededLength = count + items.count;
            if (neededLength > Elements.Length)
            {
                int newLength = Elements.Length * 2;
                if (newLength < neededLength)
                    newLength = neededLength;
                Capacity = newLength;
            }
            Array.Copy(items.Elements, 0, Elements, count, items.count);
            count = neededLength;

        }

        ///<summary>
        /// Adds a range of elements to the list from another list.
        ///</summary>
        ///<param name="items">Elements to add.</param>
        public void AddRange(List<T> items)
        {
            int neededLength = count + items.Count;
            if (neededLength > Elements.Length)
            {
                int newLength = Elements.Length * 2;
                if (newLength < neededLength)
                    newLength = neededLength;
                Capacity = newLength;
            }
            items.CopyTo(0, Elements, count, items.Count);
            count = neededLength;

        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
        public void Clear()
        {
            Array.Clear(Elements, 0, count);
            count = 0;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        public int IndexOf(T item)
        {
            return Array.IndexOf(Elements, item, 0, count);
        }


        #region IList<T> Members


        public void Insert(int index, T item)
        {
            if (index < count)
            {
                T previousValue = Elements[index];
                Elements[index] = item;
                Add(previousValue);
            }
            else
                Add(item);
        }

        public T this[int index]
        {
            get
            {
                return Elements[index];
            }
            set
            {
                Elements[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members


        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(Elements, 0, array, arrayIndex, count);
        }


        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable<T> Members

        ///<summary>
        /// Gets an enumerator for the list.
        ///</summary>
        ///<returns>Enumerator for the list.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion


        ///<summary>
        /// Sorts the list.
        ///</summary>
        ///<param name="comparer">Comparer to use to sort the list.</param>
        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(Elements, 0, count, comparer);
        }


        ///<summary>
        /// Enumerator for the RawList.
        ///</summary>
        public struct Enumerator : IEnumerator<T>
        {
            RawList<T> list;
            int index;
            ///<summary>
            /// Constructs a new enumerator.
            ///</summary>
            ///<param name="list"></param>
            public Enumerator(RawList<T> list)
            {
                index = -1;
                this.list = list;
            }
            public T Current
            {
                get { return list.Elements[index]; }
            }

            public void Dispose()
            {
            }

            object System.Collections.IEnumerator.Current
            {
                get { return list.Elements[index]; }
            }

            public bool MoveNext()
            {
                return ++index < list.count;
            }

            public void Reset()
            {
                index = -1;
            }
        }
    }
}