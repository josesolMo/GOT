using System;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCompres
{
    class NodoHuffman<T> : IComparable
    {
        public NodoHuffman<T> izqHijo { get; set; }
        public NodoHuffman<T> derHijo { get; set; }
        public NodoHuffman<T> padre { get; set; }
        public double Probability { get; set; }
        public T Value { get; set; }
        public bool Hoja { get; set; }
        public bool IsZero { get; set; }
        public bool IsRoot
        {
            get { return padre == null; }
        }
        public int Bit
        {
            get { return IsZero ? 0 : 1; }
        }
        public int CompareTo(object obj)
        {
            return -Probability.CompareTo(((NodoHuffman<T>)obj).Probability);
        }
        public NodoHuffman(double probabilidad, T valor)
        {
            Probability = probabilidad;
            izqHijo = derHijo = padre = null;
            Value = valor;
            Hoja = true;
        }
        public NodoHuffman(NodoHuffman<T> hijoIzq, NodoHuffman<T> hijoDer)
        {
            izqHijo = hijoIzq;
            derHijo = hijoDer;
            Probability = hijoIzq.Probability + hijoDer.Probability;
            hijoIzq.IsZero = true;
            derHijo.IsZero = false;
            hijoIzq.padre = hijoDer.padre = this;
            Hoja = false;
        }

    }
}
