using System;
using System.Collections.Generic;

namespace HuffmanCompres
{
    class Program
    {
        private const string Example = "abracadabrarara";
        static void Main(string[] args)
        {
            var huffman = new CodigoHuffman<char>(Example);
            List<int> encoding = huffman.Encode(Example);
            List<char> decoding = huffman.Decode(encoding);
            var outString = new string(decoding.ToArray());
            Console.WriteLine(decoding);
            Console.WriteLine(outString == Example ? "Encoding/decoding worked" : "Encoding/Decoding failed");

            var chars = new HashSet<char>(Example);
            string text = "";
            foreach (char c in chars)
            {
                encoding = huffman.Encode(c);
                Console.Write("{0}:  ", c);
                foreach (int bit in encoding)
                {
                    Console.Write("{0}", bit);
                }
                text = String.Concat(c);
                Console.Write(text);
                Console.WriteLine();
            }
            Console.Write(text);
            Console.ReadKey();
        }
    }
}
