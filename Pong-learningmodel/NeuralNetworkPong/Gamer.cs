using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    class Gamer
    {
        public Brain brain { get; }
        public Ball ball { get; }
        public float Y = 400;//Board.Height % 2;
        public const float width = 40;//Board.Width % 40 > 1 ? Board.Width % 40 : 2;
        public const float height = 160;//Board.Height % 10 > 7 ? Board.Height % 10 : 8;
        public bool IsAlive = true;
        public double LifeTime = 0;
        public double TimeRemain = 200;
        public int Balls = 1;
        //public double Score =>
        //    LifeTime * LifeTime * Math.Pow(2, Math.Min(50, Balls)) * Math.Max(1, Balls - 49);
        public double Score =>
            Math.Pow(2, Balls);
        public Gamer()
        {
            brain = new Brain();
            ball = new Ball(this);
        }
        public Gamer(Brain brain)
        {
            this.brain = brain;
            ball = new Ball(this);
        }
        public void Step()
        {
            if (!IsAlive)
                return;
            //if (LifeTime == 50000)
            //    IsAlive = false;
            LifeTime += 0.5;
            float[] observe = brain.Think(new float[] { ball.X / 800, ball.Y / 800, Y / 800 });
            if (observe[0] > observe[1])
            {
                if (Y + height / 2 + 10 <= Board.Height)
                    Y += 10;
            }
            else
            {
                if (Y - height / 2 - 10 >= 0)
                    Y -= 10;
            }
            ball.Move();
        }
        public void Draw(Graphics g)
        {
            if (IsAlive)
            {
                g.FillRectangle(Brushes.Blue, 0, Y - height / 2, width, height);
                g.FillEllipse(Brushes.Red, ball.X - Ball.size, ball.Y - Ball.size, Ball.size * 2, Ball.size * 2);
            }
        }
    }
}
