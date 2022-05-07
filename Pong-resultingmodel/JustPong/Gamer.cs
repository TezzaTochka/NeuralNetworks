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
        public int Balls = 0;
        public Gamer() : this(new Brain()) { }
        public Gamer(Brain brain)
        {
            this.brain = brain;
            ball = new Ball(this);
        }
        public void Step()
        {
            if (!IsAlive)
                return;
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
            g.FillRectangle(Brushes.Black, 0, 0, Board.Width, Board.Height);
            g.FillRectangle(Brushes.Blue, 0, Y - height / 2, width, height);
            g.FillEllipse(Brushes.Red, ball.X - Ball.size, ball.Y - Ball.size, Ball.size * 2, Ball.size * 2);
            g.DrawString($"{Balls}", new Font("Helvetica", 50), Brushes.White , new Point(2, 2));
        }
    }
}
