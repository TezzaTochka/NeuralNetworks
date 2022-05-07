using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    class Ball
    {
        public Gamer gamer;
        public float X = Board.Width - size;
        public float Y = 400;//Board.Height % 2;
        public float Angle = (float)Math.PI;
        public const float speed = 12;//Board.Width % 800 > 0 ? Board.Width % 800 : 1;
        public const float size = 20;//Board.Width % 80 > 0 ? Board.Width % 80 : 1;
        bool permit = true;
        int time = 0;
        public Ball(Gamer gamer)
        {
            this.gamer = gamer;
        }
        public void Move()
        {
            if (!gamer.IsAlive)
                return;
            if (X + (float)Math.Cos(Angle) * speed - size <= Gamer.width)
            {
                float len_x = (float)Math.Cos(Angle) * speed;
                float len_y = (float)Math.Sin(Angle) * speed;
                float ind = (X - Gamer.width - size) / (-len_x);
                float ind_y = len_y * ind;
                if (Y + ind_y + size > Board.Height)
                    ind_y -= Y + ind_y + size - Board.Height;
                else if (Y + ind_y - size < 0)
                    ind_y -= Y + ind_y - size;
                float place = Y + ind_y;
                if (place < gamer.Y - (size + Gamer.height) / 2 || place > gamer.Y + (size + Gamer.height) / 2)
                {
                    X = Gamer.width + size;
                    Y = place;
                    gamer.IsAlive = false;
                    return;
                }
                Angle += Rnd.Getweight() * (float)Math.PI / 3;
                if (Angle < (float)Math.PI / 2 + 0.2f)
                    Angle = (float)Math.PI / 2 + 0.2f;
                else if (Angle > 3 * (float)Math.PI / 2 - 0.2f)
                    Angle = 3 * (float)Math.PI / 2 - 0.2f;
                if (permit)
                {
                    permit = false;
                    gamer.Balls += 1;
                }
            }
            if (!permit)
                time++;
            if (time == 5)
            {
                time = 0;
                permit = true;
            }
            float[] move = Physics.physics(X, Y, Angle, speed, size);
            X = move[0];
            Y = move[1];
            Angle = move[2];
        }
    }
}
