using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace InsertionSortFile
{
    public class ListE : IDisposable, IEnumerable<int>
    {
        public int Head { get; private set; } = -1;
        public int Tail { get; private set; } = -1;
        public int Count { get; private set; }

        private readonly BinaryReader br;
        private readonly BinaryWriter bw;
        private readonly FileStream fn;

        public ListE(string fileName)
        {
            fn = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            bw = new BinaryWriter(fn);
            br = new BinaryReader(fn);
        }
        public int InsertionSort(ListE list, int head, int tail)
        {
            int temp = list.GetNextAdress(head);
            while (temp != null)
            {
                head = checkForPos(list, temp, head, tail);
                temp = list.GetNextAdress(temp);
            }
            return head;
        }
        public int checkForPos(ListE list, int temp, int head, int tail)
        {
            if (list.GetNodeData(temp) <= list.GetPrevAdress(list.GetNodeData(temp)) && list.GetNodeData(temp) != list.GetPrevAdress(list.GetNodeData(temp)))
            {
                int secondTemp = head;
                RemoveElement(list, temp, head, tail);
                while (list.GetNodeData(secondTemp) <= list.GetNodeData(temp))
                {
                    secondTemp = list.GetNextAdress(secondTemp);
                }
                head = InsertElement(list, temp, secondTemp, head, tail);
            }
            return head;
        }
        public int InsertElement(ListE list, int data, int before, int head, int tail)
        {
            if (before == head) { AddFirst(list.GetNodeData(data)); return head; }
            if (before == tail) { AddLast(list.GetNodeData(data)); return head; }
            //if (before != head && before != tail) AddBefore(before, list.GetNodeData(data));
            return head;
        }
        public ListE RemoveElement(ListE list, int data, int head, int tail)
        {
            if (data == tail)
            {
                tail = list.GetPrevAdress(tail);
                return this;
            }
            if (data == head)
            {
                head = list.GetNextAdress(head);
                return this;
            }
            if (data != head && data != tail)
            {
                int previous = list.GetPrevAdress(data);
                int nextas = list.GetNextAdress(data);
                //list.GetPrevAdress(nextas) = list.GetPrevAdress(data);
                //list.GetNextAdress(previous) = list.GetNextAdress(data);
            }
            return this;
        }
        public void AddFirst(int data)
        {
            if (Head == -1)
            {
                CreateNode(Count, data);
                Head = Count++;
                Tail = Head;
            }
            else
            {
                CreateNode(Count, data, -1, Head);
                SetPrevAdress(Head, Count);
                Head = Count++;
            }
        }

        public void AddLast(int data)
        {
            if (Tail == -1)
            {
                CreateNode(Count, data);
                Head = Count++;
                Tail = Head;
            }
            else
            {
                CreateNode(Count, data, Tail);
                SetNextAdress(Tail, Count);

                Tail = Count++;
            }
        }

        public int GetNodeData(int address)
        {
            br.BaseStream.Seek(address * 3 * sizeof(int), SeekOrigin.Begin);
            return br.ReadInt32();
        }
        
        public void SetNodeData(int address, int value)
        {
            bw.BaseStream.Seek(address * 3 * sizeof(int), SeekOrigin.Begin);
            bw.Write(value);
        }

        public int GetNextAdress(int address)
        {
            br.BaseStream.Seek(address * 3 * sizeof(int) + sizeof(int) * 2, SeekOrigin.Begin);
            return br.ReadInt32();
        }

        public int GetPrevAdress(int address)
        {
            br.BaseStream.Seek(address * 3 * sizeof(int) + sizeof(int), SeekOrigin.Begin);
            return br.ReadInt32();
        }

        public void SetNextAdress(int address, int value)
        {
            bw.BaseStream.Seek(address * 3 * sizeof(int) + sizeof(int)*2, SeekOrigin.Begin);
            bw.Write(value);
        }

        public void SetPrevAdress(int address, int value)
        {
            bw.BaseStream.Seek(address * 3 * sizeof(int) + sizeof(int), SeekOrigin.Begin);
            bw.Write(value);
        }
        public void CreateNode(int address, int data, int prevAddress = -1, int nextAddress = -1)
        {
            bw.BaseStream.Seek(address * 3 * sizeof(int), SeekOrigin.Begin);
            bw.Write(data);
            bw.Write(prevAddress);
            bw.Write(nextAddress);
        }
        public void Dispose() => fn.Close();
        public IEnumerator<int> GetEnumerator()
        {
            int current = Head;
            while (current != -1)
            {
                yield return GetNodeData(current);
                current = GetNextAdress(current);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int[] ToArray()
        {
            int[] arr = new int[Count];
            var en = GetEnumerator();
            for (int i = 0; i < Count; i++)
            {
                arr[i] = en.Current;
                en.MoveNext();
            }

            return arr;
        }
    }
}