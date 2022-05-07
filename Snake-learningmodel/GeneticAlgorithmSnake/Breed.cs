using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkSnake
{
    class Breed
    {
        public double TotalScore = 0;
        public const int KeepTopNum = 0;
        //public Board[] KeepTop(Board[] boards)
        //{
        //    Board[] keeptop = new Board[KeepTopNum];
        //    int[] nums = new int[boards.Length];
        //    for (int i = 0; i < boards.Length; i++)
        //        nums[i] = boards[i].snake.Score;
        //    for (int i = 0; i < KeepTopNum; i++)
        //    {
        //        keeptop[i] = boards[Array.IndexOf(nums, nums.Max())];
        //        nums[Array.IndexOf(nums, nums.Max())] = 0;
        //    }
        //    return keeptop;
        //}
        public double prov = 0;
        public Snake[] Sansara(Snake[] snakes)
        {
            double[] nums = new double[snakes.Length];
            for (int i = 0; i < snakes.Length; i++)
                nums[i] = snakes[i].Apples;
            prov = snakes[Array.IndexOf(nums, nums.Max())].Apples;
            foreach (Snake snake in snakes)
                TotalScore += snake.Score;
            Snake[] boards2 = new Snake[Game.Worlds];
            //Board[] keeptop = KeepTop(boards);
            //for (int i = 0; i < KeepTopNum; i++)
            //    boards2[i] = keeptop[i];
            for (int i = KeepTopNum; i < Game.Worlds; i++)
            {
                Brain mom = new Brain();
                if (TotalScore < 0)
                    throw new InvalidOperationException();
                double farta = Rand.GetDouble() * TotalScore;
                for (int j = 0; j < Game.Worlds; j++)
                {
                    farta -= snakes[j].Score;
                    if (farta <= 0)
                    {
                        mom = snakes[j].brain;
                        break;
                    }
                }
                Brain dad = new Brain();
                farta = Rand.GetDouble() * TotalScore;
                for (int j = 0; j < Game.Worlds; j++)
                {
                    farta -= snakes[j].Score;
                    if (farta <= 0)
                    {
                        dad = snakes[j].brain;
                        break;
                    }
                }
                boards2[i] = new Snake(Brain.Cross(mom, dad));
            }
            TotalScore = 0;
            return boards2;
        }
    }
}
