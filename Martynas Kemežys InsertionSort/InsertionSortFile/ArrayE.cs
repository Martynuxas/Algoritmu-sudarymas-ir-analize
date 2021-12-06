using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace InsertionSortFile
{
    public class ArrayE : IDisposable, IEnumerable<int>
    {
        private readonly BinaryReader br;
        private readonly BinaryWriter bw;
        private readonly FileStream fn;
        public int Length { get; private set; }

        private int elementSize;
        public ArrayE(string fileName, int size = sizeof(int), int iLength = 0)
        {
            elementSize = size;
            Length = iLength;

            fn = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            bw = new BinaryWriter(fn);
            br = new BinaryReader(fn);
        }

        public void Add(int element)
        {
            bw.Write(element);
            Length++;
        }
        public int this[int index]{
            get{
                br.BaseStream.Seek(index * elementSize, SeekOrigin.Begin);
                return br.ReadInt32();
            }
            set{
                bw.BaseStream.Seek(index * elementSize, SeekOrigin.Begin);
                bw.Write(value);
            }
        }
        public void Dispose() => fn.Close();

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
                yield return this[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int[] ToArray()
        {
            int[] arr = new int[Length];
            var en = GetEnumerator();
            for (int i = 0; i < Length; i++)
            {
                arr[i] = en.Current;
                en.MoveNext();
            }

            return arr;
        }
    }
}