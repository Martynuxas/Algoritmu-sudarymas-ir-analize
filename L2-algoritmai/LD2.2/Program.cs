using System;
using System.Diagnostics;
using System.Linq;

namespace LD2_0_2
{
    internal class Program
    {
        public static int Kp(int n, int W, int[] s, int[] p)
        {
            if (n == 0 || W == 0) return 0;
            else if (s[n - 1] > W) return Kp(n - 1, W, s, p);
            else return Math.Max(Kp(n - 1, W, s, p), p[n - 1] + Kp(n - 1, W - s[n - 1], s, p));
        }
        
    public static int KpBottomUp(int k, int W, int[] s, int[] p)
    {
        int n = s.Length;                                
        int[,] arr = new int[n + 1, W + 1];             
        
        //Nusinuliname sąrašą
        for (int i = 0; i < n; i++) {                   
            for (int j = 0; j < W; j++) {                
                arr[i,j] = 0;                           
            }
        }
        for (int i = 1; i <= n; i++) {                  
            for (int j = 0; j <= W; j++) {              
                int weight = s[i - 1];                  
                if (weight <= j) {                      
                    arr[i,j] = Math.Max(               
                            p[i - 1] + arr[i - 1,j - weight],
                            arr[i - 1,j]
                    );
                } else {                              
                    arr[i,j] = arr[i - 1,j];          
                }
            }
        }
        return arr[n,W];          
    }
    private static int[] GetArray(int length, int till)
    {
        var arr = new int[length];
        Random rand = new Random();
        for (int i = 0; i < length; i++)
            arr[i] = rand.Next(till);;
        return arr;
    }
    public static void Main(String[] args) {
        int[] p = GetArray(400, 100);
        int[] s = GetArray(400, 30);
        int n = 10;
        int W = n*4;
        for (int i = 1; i < 10; i++)
        { 
            n = 10*i;
        Stopwatch st = Stopwatch.StartNew();
        Console.WriteLine(Kp(n, W, s, p));
        
        st.Stop();
        Console.WriteLine($"  {st.Elapsed};    {n};");
        st.Restart();
        Console.WriteLine(KpBottomUp(n - 1, W, s, p));
        Console.WriteLine($"  {st.Elapsed};    {n};");
        st.Stop();
        }
    }
}
}