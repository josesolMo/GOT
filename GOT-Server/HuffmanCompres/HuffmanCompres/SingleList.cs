using System;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCompres
{
    class SingleList
    {
        private LinkedNode head;
        public void printAllNodes()
        {
            LinkedNode current = head;
            while (current != null)
            {
                Console.WriteLine(value: current.data);
                current = current.next;
            }
        }
        public void Add(CodigoHuffman<char> objetoHuffman)
        {
            LinkedNode toAdd = new LinkedNode();
            toAdd.data = objetoHuffman;
            LinkedNode current = head;
            current.next = toAdd;
        }
        public void addEnd(CodigoHuffman<char> objetoHuffman)
        {
            if (head == null)
            {
                head = new LinkedNode();
                head.data = objetoHuffman;
                head.next = null;
            }
            else
            {
                LinkedNode toAdd = new LinkedNode();
                toAdd.data = objetoHuffman;

                LinkedNode current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = toAdd;
            }
        }
        public void Delete(CodigoHuffman<char> objetoHuffman)
        {
            LinkedNode node = head;
            LinkedNode currNode = head;
            LinkedNode prevNode = null;
            while (node != null)
            {
                currNode = node;
                if (node.data == objetoHuffman)
                {
                    if (prevNode != null)
                    {
                        prevNode.next = currNode.next;
                    }
                    else
                    {
                        head = head.next;
                    }
                    break;
                }
                prevNode = currNode;
                node = node.next;
            }
        }

    }
}
