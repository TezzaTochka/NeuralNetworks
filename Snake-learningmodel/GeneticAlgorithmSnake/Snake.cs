using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkSnake
{
    class Snake
    {
        public static int Width { get; } = 40;
        public static int Height { get; } = 40;
        public static int Size { get; } = 23;
        public static bool Contains(int[] pos)
        {
            if (pos[0] >= 0 && pos[0] < Width && pos[1] >= 0 && pos[1] < Height)
                return true;
            else
                return false;
        }
        public const int InitialLength = 6;
        public const int InitialTimeRemaining = 200;
        public const int BonusTime = 100;
        public List<int[]> body = new List<int[]> { };
        public bool IsAlive { get; set; } = true;
        public int[] head;
        public Brain brain { get; }
        public double LifeTime = 0;
        public int TimeRemain = InitialTimeRemaining;
        public double Apples = 0;
        public int[] Apple;
        public int[] SpawnApple()
        {
            while (true)
            {
                int[] pos = new int[] { Rand.GetInt(Width), Rand.GetInt(Height) };
                if (!Contains(body, pos))
                    return pos;
            }
        }
        static int[] UP { get; } = { 0, -1 };
        static int[] DOWN { get; } = { 0, 1 };
        static int[] RIGHT { get; } = { 1, 0 };
        static int[] LEFT { get; } = { -1, 0 };

        static int[] UPRIGHT { get; } = { 1, -1 };
        static int[] UPLEFT { get; } = { -1, -1 };
        static int[] DOWNRIGHT { get; } = { 1, 1 };
        static int[] DOWNLEFT { get; } = { -1, 1 };

        public static int[][] FourDirections { get; } = { UP, DOWN, RIGHT, LEFT };
        static int[][] EightDirections { get; } = { UP, DOWN, RIGHT, LEFT, UPRIGHT, UPLEFT, DOWNRIGHT, DOWNLEFT };

        public double Score =>
            LifeTime * LifeTime * Math.Pow(2, Math.Min(10, Apples)) * Math.Max(1, Apples - 9);
        public Snake()
        {
            brain = new Brain();
            int[] pos = { Rand.GetInt(Width), Rand.GetInt(Height) };
            for (int i = 0; i < InitialLength; i++)
                body.Add(pos);
            head = body[0];
            Apple = SpawnApple();
        }
        public Snake(Brain brain)
        {
            this.brain = brain;
            int[] pos = { Rand.GetInt(Width), Rand.GetInt(Height) };
            for (int i = 0; i < InitialLength; i++)
                body.Add(pos);
            head = body[0];
            Apple = SpawnApple();
        }
        public void Step()
        {
            if (!IsAlive)
                return;
            LifeTime++;
            TimeRemain--;
            float[] vib = brain.Think(Observe());
            int MaxIndex = 0;
            for (int i = 1; i < 4; i++)
                if (vib[i] > vib[MaxIndex])
                    MaxIndex = i;
            int[] dir = new int[2];
            dir[0] = FourDirections[MaxIndex][0];
            dir[1] = FourDirections[MaxIndex][1];
            Move(dir);
        }
        public static bool Contains(List<int[]> list, int[] pos)
        {
            foreach (int[] sig in list)
                if (sig[0] == pos[0] && sig[1] == pos[1])
                    return true;
            return false;
        }
        private void Move(int[] dir)
        {
            int[] newhead = new int[] { head[0] + dir[0], head[1] + dir[1] };

            if (newhead[0] == body[1][0] && newhead[1] == body[1][1])
            {
                if (dir[0] == 0)
                    dir[1] *= -1;
                else
                    dir[0] *= -1;
                //if (dir[0] == UP[0] && dir[1] == UP[1])
                //    dir = DOWN;
                //else if (dir[0] == DOWN[0] && dir[1] == DOWN[1])
                //    dir = UP;
                //else if (dir[0] == RIGHT[0] && dir[1] == RIGHT[1])
                //    dir = LEFT;
                //else if (dir[0] == LEFT[0] && dir[1] == LEFT[1])
                //    dir = RIGHT;
                newhead = new int[] { head[0] + dir[0], head[1] + dir[1] };
            }
            if (!Contains(newhead) || TimeRemain == 0 || Contains(body, newhead))
            {
                IsAlive = false;
                return;
            }
            int seg = body.Count() - 1;
            if (newhead[0] == Apple[0] && newhead[1] == Apple[1])
            {
                seg++;
                Apples++;
                TimeRemain += BonusTime;
                Apple = SpawnApple();
            }
            List<int[]> newbody = new List<int[]> { newhead };
            for (int i = 0; i < seg; i++)
                newbody.Add(body[i]);
            body = newbody;
            head = body[0];
        }
        private List<float> Observe()
        {
            List<float> observe = new List<float> { };
            foreach (int[] dir in EightDirections)
            {
                observe.Add(LengthToBoard(dir));
                observe.Add(LengthToTail(dir));
                observe.Add(LengthToApple(dir));
            }
            return observe;
        }
        private float LengthToBoard(int[] dir)
        {
            for (int i = 1; true; i++)
                if (!Contains(new int[] { head[0] + dir[0] * i, head[1] + dir[1] * i }))
                    return 1f / i;
        }
        private float LengthToTail(int[] dir)
        {
            for (int i = 1; true; i++)
            {
                if (!Contains(new int[] { head[0] + dir[0] * i, head[1] + dir[1] * i }))
                    return 0;
                else if (body.Contains(new int[] { head[0] + dir[0] * i, head[1] + dir[1] * i }))
                    return 1f / i;
                //else if (Contains(body, new int[] { head[0] + dir[0] * i, head[1] + dir[1] * i }))
                //    return 1f / i;
            }
        }
        private float LengthToApple(int[] dir)
        {
            for (int i = 1; true; i++)
            {
                if (!Contains(new int[] { head[0] + dir[0] * i, head[1] + dir[1] * i }))
                    return 0;
                else if (Apple[0] == head[0] + dir[0] * i && Apple[1] == head[1] + dir[1] * i)
                    return 1f / i;
            }
        }
    }
}
