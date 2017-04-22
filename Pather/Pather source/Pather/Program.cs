using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pather
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo curDir = new DirectoryInfo(Environment.CurrentDirectory);
            FileInfo[] Files = curDir.GetFiles("input" + "*.txt");
            foreach (var file in Files)
            {
                string readText = File.ReadAllText(file.FullName);
                string[] lines = readText.Split('\n');
                int n = lines.Length;
                char[][] points = new char[n][];
                int[][] pathPoints = new int[readText.Count(t => t == '#')][];

                for (int i = 0; i < n; i++)
                    points[i] = lines[i].ToCharArray();

                for (int i = 0, k = 0; i < lines.Length; i++)
                    if (!String.IsNullOrEmpty(lines[i]))
                        for (int j = 0; j < lines[0].Length; j++)
                        {
                            if (lines[i][j].Equals('#'))
                                pathPoints[k++] = new int[2] { i, j };
                        }
                for (int i = 0; i < pathPoints.Length - 1; i++)
                {
                    int maxX = pathPoints[i][0] > pathPoints[i + 1][0] ? pathPoints[i][0] : pathPoints[i + 1][0];
                    int minX = pathPoints[i][0] < pathPoints[i + 1][0] ? pathPoints[i][0] : pathPoints[i + 1][0];
                    int maxY = pathPoints[i][1] > pathPoints[i + 1][1] ? pathPoints[i][1] : pathPoints[i + 1][1];
                    int minY = pathPoints[i][1] < pathPoints[i + 1][1] ? pathPoints[i][1] : pathPoints[i + 1][1];
                    for (int j = minX; j <= maxX && j < points.Length; j++)
                        points[j][pathPoints[i][1]] = '*';
                    for (int j = minY; j <= maxY && j < points[pathPoints[i + 1][0]].Length; j++)
                        points[pathPoints[i + 1][0]][j] = '*';
                }
                foreach (var p in pathPoints)
                    points[p[0]][p[1]] = '#';
                for (int i = 0; i < lines.Length; i++)
                    lines[i] = new string(points[i]);

                string result = String.Join("\n", lines);
                File.WriteAllText(file.FullName.Replace("input", "output"), result);
            }
        }
    }
}
