using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dijkstra_Single
{
    class Program
    {
        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;
 
            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }
 
            return minIndex;
        }
 
        private static void Print(int[] distance, int verticesCount)
        {
            Console.WriteLine("Vertex    Distance from source");
 
            for (int i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}", i, distance[i]);
        }
 
        public static void DijkstraAlgo(int[,] graph, int source, int verticesCount)
        {
            System.Diagnostics.Stopwatch cloky = new System.Diagnostics.Stopwatch();
            cloky.Start();
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];
 
            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }
 
            distance[source] = 0;
 
            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;
 
                for (int v = 0; v < verticesCount; ++v)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                        distance[v] = distance[u] + graph[u, v];
            }
            cloky.Stop();
            Print(distance, verticesCount);
            Console.WriteLine("Time spended: " + cloky.Elapsed);
        }

        static int[,] arrinit(string filepath)
        {
            
            StreamReader file = new StreamReader(filepath);
            string s = file.ReadToEnd();
            file.Close();
            string[] st = s.Split('\n');
            string[] cl = st[0].Split(' ');
            int[,] a = new int[st.Length, cl.Length];
            int t = 0;
            for (int i = 0; i < st.Length; i++)
            {
                cl = st[i].Split(' ');
                for (int j = 0; j < cl.Length; j++)
                {
                    t = Convert.ToInt32(cl[j]);
                    a[i, j] = t;
                }
            }
            return a;
        }
 
        static void Main(string[] args)
        {
            string filepath = args[0];
            int[,] graph =  arrinit(filepath);
            DijkstraAlgo(graph, 0, graph.GetLength(0));
        }
    }
    }
