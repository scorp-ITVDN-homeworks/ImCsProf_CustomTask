using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomArray
{
    public class CustomArray<T> : IEnumerable<T>, IEnumerator<T>//, IList<T>
    {
        private int first;
        /// <summary>
        /// Should return first index of array
        /// </summary>
        public int First 
        { 
            get { return first; }
            private set { first = value; }
        }

        /// <summary>
        /// Should return last index of array
        /// </summary>
        public int Last 
        { 
            get { return First + Length - 1; }
        }

        private int length;
        /// <summary>
        /// Should return length of array
        /// <exception cref="ArgumentException">Thrown when value was smaller than 0</exception>
        /// </summary>
        public int Length 
        {   
            get { return length;}
            private set 
            {
                if (value < 0) throw new ArgumentException("Value smaller than 0");
                length = value; 
            }
        }

        private T[] customArray;
        /// <summary>
        /// Should return array 
        /// </summary>
        public T[] Array
        { 
            get { return customArray; } 
        }



        /// <summary>
        /// Constructor with first index and length
        /// </summary>
        /// <param name="first">First Index</param>
        /// <param name="length">Length</param>         
        public CustomArray(int first, int length)
        {
            First = first;
            Length = length;
            customArray = new T[] { };
            System.Array.Resize(ref customArray, length);
            position = First - 1;
        }


        /// <summary>
        /// Constructor with first index and collection  
        /// </summary>
        /// <param name="first">First Index</param>
        /// <param name="list">Collection</param>
        ///  <exception cref="NullReferenceException">Thrown when list is null</exception>
        /// <exception cref="ArgumentException">Thrown when count is smaler than 0</exception>
        public CustomArray(int first, IEnumerable<T> list)
        {
            if (list == null) throw new NullReferenceException();
            this.first = first;
            customArray = new T[] { };
            int size = 0;
            foreach(T item in list)
            {
                size++;
                System.Array.Resize(ref customArray, size);
                customArray[size - 1] = item;
                // зачем, если это же исключение всплывает при установке свойства?
                // нет и не будет таких обстоятельств когда Length установится в значение меньше 0
                if (size < 0) throw new ArgumentException();
                Length = size;
            }
            position = First - 1;
        }

        /// <summary>
        /// Constructor with first index and params
        /// </summary>
        /// <param name="first">First Index</param>
        /// <param name="list">Params</param>
        ///  <exception cref="ArgumentNullException">Thrown when list is null</exception>
        /// <exception cref="ArgumentException">Thrown when list without elements </exception>
        public CustomArray(int first, params T[] list) : this(first, list as IEnumerable<T>)
        {
            
        }

        /// <summary>
        /// Indexer with get and set  
        /// </summary>
        /// <param name="item">Int index</param>        
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when index out of array range</exception> 
        /// <exception cref="ArgumentNullException">Thrown in set  when value passed in indexer is null</exception>
        public T this[int index]
        {
            get
            {                
                try
                {
                    if (customArray[index - First] == null) throw new ArgumentNullException();
                    return customArray[index - First];
                }
                catch
                {
                    throw new ArgumentException();
                }
            }
            set
            {
                try
                {
                    customArray[index - First] = value;
                }
                catch
                {
                    throw new ArgumentException();
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Reset();
            return this;
        }

        public T Current
        {
            get { return this[position]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }


        public bool MoveNext()
        {
            position++;
            if(position > Last)
            {
                Reset();
                return false;
            }
            return true;
        }

        private int position;
        public void Reset()
        {
            position = First - 1;
        }

        public void Dispose()
        {
            // не умею в сборку мусора
        }


        #region IList<T> realization
        /* IList generic interface can't be impemented
         * by the conditions of the task
         * But some methods can
         */
        public int IndexOf(T item)
        {
            return System.Array.IndexOf(customArray, item);
        }

        public void Insert(int index, T item)
        {
            try
            {
                customArray[index] = item;
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public bool Contains(T item)
        {
            return System.Array.Exists(customArray, x => x.Equals(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            try
            {
                foreach(T element in array)
                {
                    customArray[arrayIndex] = element;
                }
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        //// Length property already exists
        //public int Count => throw new NotImplementedException();

        //// Not neccessery by condition of the task
        //public bool IsReadOnly => throw new NotImplementedException();

        //// this method not support conditions of the task 
        //public void RemoveAt(int index)
        //{
        //    throw new NotImplementedException();
        //}

        //// this method not support conditions of the task 
        //public void Add(T item)
        //{
        //    throw new NotImplementedException();
        //    // this method not support conditions of task 
        //    //System.Array.Resize(ref peopleInQueue, Count + 1);
        //    //peopleInQueue[Count - 1] = value as Citizen;
        //}

        //// this method not support conditions of the task 
        //public void Clear()
        //{
        //    throw new NotImplementedException();
        //}

        //// this method not support conditions of the task 
        //public bool Remove(T item)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
