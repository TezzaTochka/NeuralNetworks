using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    static class Physics
    {
        public static float[] physics(float x, float y, float angle, float speed, float size)
        {
            float len_x = (float)Math.Cos(angle) * speed;
            float len_y = (float)Math.Sin(angle) * speed;
            if (x + len_x + size > Board.Width)
            {
                if (len_y >= 0)
                    angle = (float)Math.PI - angle;
                else
                    angle = (float)Math.PI * 3 - angle;
                len_x -= x + len_x + size - Board.Width;
            }
            else if (x + len_x - size < Gamer.width)
            {
                if (len_y >= 0)
                    angle = (float)Math.PI - angle;
                else
                    angle = (float)Math.PI * 3 - angle;
                len_x -= x + len_x - size - Gamer.width;
            }
            if (y + len_y + size > Board.Height)
            {
                angle = (float)Math.PI * 2 - angle;
                len_y -= y + len_y + size - Board.Height;
            }
            else if (y + len_y - size < 0)
            {
                angle = (float)Math.PI * 2 - angle;
                len_y -= y + len_y - size;
            }
            x += len_x;
            y += len_y;
            return new float[] { x, y , angle};
        }
    }
}
