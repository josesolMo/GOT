using System;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCompres
{
    class ColadePrioridad<T> where T : IComparable
    {
        protected List<T> lista = new List<T>();

        public virtual int Count
        {
            get { return lista.Count; }
        }
        public virtual void Add(T value)
        {
            lista.Add(value);
            SetAt(lista.Count - 1, value);
            UpHeap(lista.Count - 1);
        }

        public virtual T Peek()
        {
            if (lista.Count == 0)
            {
                throw new IndexOutOfRangeException("Peeking en cola da prioridad vacia");
            }
            return lista[0];
        }
        public virtual T Pop()
        {
            if (lista.Count == 0)
            {
                throw new IndexOutOfRangeException("Popping en cola da prioridad vacia");
            }
            T valReturn = lista[0];
            SetAt(0, lista[lista.Count - 1]);
            lista.RemoveAt(lista.Count - 1);
            DownHeap(0);
            return valReturn;
        }

        public virtual void SetAt(int i, T value)
        {
            lista[i] = value;
        }
        public bool RightSonExists(int i)
        {
            return RightChildIndex(i) < lista.Count;
        }
        public bool LeftSonExists(int i)
        {
            return LeftChildIndex(i) < lista.Count;
        }
        public int ParentIndex(int i)
        {
            return (i - 1) / 2;
        }
        public int LeftChildIndex(int i)
        {
            return 2 * i + 1;
        }
        public int RightChildIndex(int i)
        {
            return 2 * (i + 1);
        }
        public T ArrayVal(int i)
        {
            return lista[i];
        }
        public T Parent(int i)
        {
            return lista[ParentIndex(i)];
        }
        public T Left(int i)
        {
            return lista[LeftChildIndex(i)];
        }
        public T Right(int i)
        {
            return lista[RightChildIndex(i)];
        }
        public void Swap(int i, int j)
        {
            T temp = ArrayVal(i);
            SetAt(i, lista[j]);
            SetAt(j, temp);
        }
        public void UpHeap(int i)
        {
            while (i > 0 && ArrayVal(i).CompareTo(Parent(i)) > 0)
            {
                Swap(i, ParentIndex(i));
                i = ParentIndex(i);
            }
        }
        public void DownHeap(int i)
        {
            while (i >= 0)
            {
                int iContinue = -1;
                if (RightSonExists(i) && Right(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinue = Left(i).CompareTo(Right(i)) < 0 ? RightChildIndex(i) : LeftChildIndex(i);
                }
                else if (LeftSonExists(i) && Left(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinue = LeftChildIndex(i);
                }
                if (iContinue >= 0 && iContinue < lista.Count)
                {
                    Swap(i, iContinue);
                }
                i = iContinue;
            }
        }
    }
}
