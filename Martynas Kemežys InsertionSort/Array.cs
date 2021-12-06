namespace InsertionSort
{
    public class Array
    {
        public static void InsertionSort(int[] data)
        {
            int i, key, j;
            for (i = 1; i < data.Length; i++)
            {
                key = data[i];
                j = i - 1;
                while (j >= 0 && data[j] > key)
                {
                    data[j + 1] = data[j];
                    j --;
                }
                data[j + 1] = key;
            }
        }
    }
}
