using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace RayTracing1
{
    public class OBJReader
    {
        public static List<Trangle> ReadFile(string FilePath)
        {
            List<vec3s> PointList = new List<vec3s>();
            List<Trangle> TempList = new List<Trangle>();
            string[] lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
            {
                if (line.Length == 0) continue;
                if (line[0] == 'v')
                {
                    float p1 = float.Parse(line.Replace(" ", "#").Split('#')[1]);
                    float p2 = float.Parse(line.Replace(" ", "#").Split('#')[2]);
                    float p3 = float.Parse(line.Replace(" ", "#").Split('#')[3]);

                    if (line.Replace(" ", "#").Split('#').Length >= 6)
                    {
                        float c1 = float.TryParse(line.Replace(" ", "#").Split('#')[4], out var result1) ? result1 / 255f : 1;
                        float c2 = float.TryParse(line.Replace(" ", "#").Split('#')[5], out var result2) ? result2 / 255f : 1;
                        float c3 = float.TryParse(line.Replace(" ", "#").Split('#')[6], out var result3) ? result3 / 255f : 1;
                        PointList.Add(new vec3s(p1, p2, p3) { c1 = c1, c2 = c2, c3 = c3 });
                    }
                    else
                    {
                        PointList.Add(new vec3s(p1, p2, p3));
                    }

                }
            }

            foreach (string line in lines)
            { 
                if (line.Length == 0) continue;
                if (line[0] == 'f')
                { 
                    int p1 = int.Parse(line.Replace(" ", "#").Split('#')[1]);
                    int p2 = int.Parse(line.Replace(" ", "#").Split('#')[2]);
                    int p3 = int.Parse(line.Replace(" ", "#").Split('#')[3]);
                    Trangle Temp=new Trangle();
                    Temp.p1 = PointList[p1 - 1].Clone();
                    Temp.p2 = PointList[p2 - 1].Clone();
                    Temp.p3 = PointList[p3 - 1].Clone();
                    TempList.Add(Temp);
                }
            }
            return TempList;
        }
    }
}
