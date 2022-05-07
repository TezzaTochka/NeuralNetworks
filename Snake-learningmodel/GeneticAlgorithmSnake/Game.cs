using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace NeuralNetworkSnake
{
    class Game
    {
        public const int Worlds = 100;
        public int generation = 1;
        public int TimeFrame = 1;
        public Snake[] snakes = new Snake[Worlds];
        Breed breed = new Breed();
        public int trebs = 150;
        public Game()
        {
            for (int i = 0; i < Worlds; i++)
                snakes[i] = new Snake();
        }
        public void Draw(Graphics g)
        {
            Clear(g);
            for (int i = 0; i < Worlds; i++)
            {
                if (snakes[i].IsAlive)
                    DrawApple(g, snakes[i].Apple[0], snakes[i].Apple[1]);
            }
            for (int i = 0; i < Worlds; i++)
            {
                if (snakes[i].IsAlive)
                    foreach (int[] pos in snakes[i].body)
                        DrawCell(g, Brushes.Green, pos[0], pos[1]);
            }
            g.DrawString($"Поколение: {generation}\r\nЯблоки: {breed.prov}", new Font("Times New Roman", 24), Brushes.White, new Point(2, 2));
        }
        public void Clear(Graphics g) =>
            g.FillRectangle(Brushes.Black, 0, 0, Snake.Width * Snake.Size, Snake.Height * Snake.Size);
        public void DrawApple(Graphics g, int x, int y) =>
            g.FillEllipse(Brushes.Red, x * Snake.Size, y * Snake.Size, Snake.Size - 1, Snake.Size - 1);
        public void DrawCell(Graphics g, Brush brush, int x, int y) =>
            g.FillRectangle(brush, x * Snake.Size, y * Snake.Size, Snake.Size - 1, Snake.Size - 1);
        public void Step()
        {
            //int ind = 0;
            //for (int i = 0; i < Worlds; i++)
            //    if (boards[i].snake.Apples >= trebs)
            //        ind = i;
            //if (boards[ind].snake.Apples >= trebs)
            //{
            //    string subtext;
            //    string text1 = "{ ";
            //    float[,] list = boards[ind].snake.brain.InToF.Cells;
            //    for (int i = 0; i < list.GetLength(0); i++)
            //    {
            //        subtext = "{ ";
            //        for (int j = 0; j < list.GetLength(1); j++)
            //            subtext += Convert.ToString(list[i, j]).Replace(',', '.') + "f, ";
            //        subtext = subtext.Remove(subtext.Length - 2);
            //        subtext += " }, ";
            //        text1 += subtext;
            //    }
            //    text1 = text1.Remove(text1.Length - 2);
            //    text1 += " }";

            //    string text2 = "{ ";
            //    list = boards[ind].snake.brain.FToG.Cells;
            //    for (int i = 0; i < list.GetLength(0); i++)
            //    {
            //        subtext = "{ ";
            //        for (int j = 0; j < list.GetLength(1); j++)
            //            subtext += Convert.ToString(list[i, j]).Replace(',', '.') + "f, ";
            //        subtext = subtext.Remove(subtext.Length - 2);
            //        subtext += " }, ";
            //        text2 += subtext;
            //    }
            //    text2 = text2.Remove(text2.Length - 2);
            //    text2 += " }";

            //    string text3 = "{ ";
            //    list = boards[ind].snake.brain.GToOut.Cells;
            //    for (int i = 0; i < list.GetLength(0); i++)
            //    {
            //        subtext = "{ ";
            //        for (int j = 0; j < list.GetLength(1); j++)
            //            subtext += Convert.ToString(list[i, j]).Replace(',', '.') + "f, ";
            //        subtext = subtext.Remove(subtext.Length - 2);
            //        subtext += " }, ";
            //        text3 += subtext;
            //    }
            //    text3 = text3.Remove(text3.Length - 2);
            //    text3 += " }";

            //    File.WriteAllText(@"C:\Users\rahma\Desktop\weight_snake.txt", text1 + "\n\n" + text2 + "\n\n" + text3 + "\n");
            //}
            Parallel.ForEach(snakes, snake =>
            {
                for (int j = 0; j < TimeFrame; j++)
                {
                    snake.Step();
                    if (!snake.IsAlive)
                        break;
                }
            });
            if (!snakes.Any(snake => snake.IsAlive))
            {
                snakes = breed.Sansara(snakes);
                generation++;
            }
        }
    }
}
