using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HuffmanCompres
{
    class CodigoHuffman<T> where T : IComparable
    {
        private readonly Dictionary<T, NodoHuffman<T>> _HojasDictionary = new Dictionary<T, NodoHuffman<T>>();
        private readonly NodoHuffman<T> _root;

        public CodigoHuffman(IEnumerable<T> values)
        {
            var conteos = new Dictionary<T, int>();
            var colaPrioridad = new ColadePrioridad<NodoHuffman<T>>();
            int valorConteo = 0;

            foreach (T value in values)
            {
                if (!conteos.ContainsKey(value))
                {
                    conteos[value] = 0;
                }
                conteos[value]++;
                valorConteo++;
            }

            foreach (T value in conteos.Keys)
            {
                var node = new NodoHuffman<T>((double)conteos[value] / valorConteo, value);
                colaPrioridad.Add(node);
                _HojasDictionary[value] = node;
            }

            while (colaPrioridad.Count > 1)
            {
                NodoHuffman<T> leftSon = colaPrioridad.Pop();
                NodoHuffman<T> rightSon = colaPrioridad.Pop();
                var parent = new NodoHuffman<T>(leftSon, rightSon);
                colaPrioridad.Add(parent);
            }

            _root = colaPrioridad.Pop();
            _root.IsZero = false;
        }

        public List<int> Encode(T value)
        {
            var returnValue = new List<int>();
            Encode(value, returnValue);
            return returnValue;
        }
        public void Encode(T value, List<int> encoding)
        {
            if (!_HojasDictionary.ContainsKey(value))
            {
                throw new ArgumentException("Invalid value at Encode");
            }
            NodoHuffman<T> nodeCur = _HojasDictionary[value];
            var reverseEncoding = new List<int>();
            while (!nodeCur.IsRoot)
            {
                reverseEncoding.Add(nodeCur.Bit);
                nodeCur = nodeCur.padre;
            }

            reverseEncoding.Reverse();
            encoding.AddRange(reverseEncoding);
        }
        public List<int> Encode(IEnumerable<T> values)
        {
            var returnValue = new List<int>();
            foreach (T value in values)
            {
                Encode(value, returnValue);
            }
            return returnValue;
        }
        public T Decode(List<int> bitString, ref int position)
        {
            NodoHuffman<T> nodeCur = _root;
            while (!nodeCur.Hoja)
            {
                if (position > bitString.Count)
                {
                    throw new ArgumentException("Invalid bitstring in Decode");
                }
                nodeCur = bitString[position++] == 0 ? nodeCur.izqHijo : nodeCur.derHijo;
            }
            return nodeCur.Value;
        }
        public List<T> Decode(List<int> bitString)
        {
            int position = 0;
            var returnValue = new List<T>();
            while (position != bitString.Count)
            {
                returnValue.Add(Decode(bitString, ref position));
            }
            return returnValue;
        }
    }
}
