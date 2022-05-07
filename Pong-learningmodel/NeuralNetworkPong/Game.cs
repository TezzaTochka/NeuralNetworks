using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    class Game
    {
        public static int Worlds = 100;
        public int FrameWork = 1;
        public int generations = 1;
        public Breed breed = new Breed();
        public Gamer[] gamers = new Gamer[Worlds];
        double schet = 0;
        public Game()
        {
            for (int i = 0; i < Worlds; i++)
                gamers[i] = new Gamer();
        }
        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Black, 0, 0, Board.Width, Board.Height);
            foreach (Gamer gamer in gamers)
                gamer.Draw(g);
            g.DrawString($"{generations}\r\n{Breed.maxballs}", new Font("Helvetica", 50), Brushes.White, new Point(2, 2));
        }
        public void Step()
        {
            schet++;
            if (schet % 1000 == 0)
            {
                int ind = new int();
                for (int i = 0; i < Worlds; i++)
                    if (gamers[i].IsAlive)
                    {
                        ind = i;
                        break;
                    }
                string text1 = "{";
                float[,] list = gamers[ind].brain.InputToF.Cells;
                for (int i = 0; i < list.GetLength(0); i++)
                {
                    string subtext = " {";
                    for (int j = 0; j < list.GetLength(1); j++)
                        subtext += " " + Convert.ToString(list[i, j]).Replace(',', '.') + "f, ";
                    subtext = subtext.Remove(subtext.Length - 2);
                    subtext += " }, ";
                    text1 += subtext;
                }
                text1 = text1.Remove(text1.Length - 2);
                text1 += " }";

                string text2 = "{";
                list = gamers[ind].brain.FToG.Cells;
                for (int i = 0; i < list.GetLength(0); i++)
                {
                    string subtext = " {";
                    for (int j = 0; j < list.GetLength(1); j++)
                        subtext += " " + Convert.ToString(list[i, j]).Replace(',', '.') + "f, ";
                    subtext = subtext.Remove(subtext.Length - 2);
                    subtext += " }, ";
                    text2 += subtext;
                }
                text2 = text2.Remove(text2.Length - 2);
                text2 += " }";

                string text3 = "{";
                list = gamers[ind].brain.GToOutput.Cells;
                for (int i = 0; i < list.GetLength(0); i++)
                {
                    string subtext = " {";
                    for (int j = 0; j < list.GetLength(1); j++)
                        subtext += " " + Convert.ToString(list[i, j]).Replace(',', '.') + "f, ";
                    subtext = subtext.Remove(subtext.Length - 2);
                    subtext += " }, ";
                    text3 += subtext;
                }
                text3 = text3.Remove(text3.Length - 2);
                text3 += " }";

                File.WriteAllText(@"C:\Users\rahma\Desktop\weight_pong.txt", text1 + "\n" + text2 + "\n" + text3);
            }
            Parallel.ForEach(gamers, game =>
            {
                for (int i = 0; i < FrameWork; i++)
                {
                    game.Step();
                    if (!game.IsAlive)
                        break;
                }
            });
            if (!gamers.Any(gamer => gamer.IsAlive))
            {
                schet = 0;
                gamers = Breed.Sansara(gamers);
                generations += 1;
            }
        }
    }
}
