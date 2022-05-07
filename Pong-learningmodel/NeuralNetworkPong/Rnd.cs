using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    static class Rnd
    {
        private static Random rnd = new Random();
        public static float GetInt(int y, int x = 0) => rnd.Next(x, y);
        public static double GetDouble() => rnd.NextDouble();
        public static float GetFloat() => (float)GetDouble();
        public static float Getweight() => GetFloat() * (GetInt(2) == 0 ? 1 : -1);
    }
}
