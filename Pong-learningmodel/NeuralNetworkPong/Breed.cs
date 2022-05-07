using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    class Breed
    {
        public static double TotalScore = 0;
        public static int maxballs = 0;
        public static Gamer[] Sansara(Gamer[] gamers)
        {
            int[] numsballs = new int[Game.Worlds];
            int k = 0;
            foreach (Gamer gamer in gamers)
            {
                TotalScore += gamer.Score;
                numsballs[k] = gamer.Balls;
                k++;
            }
            maxballs = gamers[Array.IndexOf(numsballs, numsballs.Max())].Balls - 1;
            Gamer[] newgamers = new Gamer[Game.Worlds];
            Brain mom = new Brain();
            Brain dad = new Brain();
            for (int i = 0; i < Game.Worlds; i++)
            {
                double Farta = Rnd.GetFloat() * TotalScore;
                foreach(Gamer gamer in gamers)
                {
                    Farta -= gamer.Score;
                    if (Farta <= 0)
                        mom = gamer.brain;
                }
                Farta = Rnd.GetFloat() * TotalScore;
                foreach(Gamer gamer in gamers)
                {
                    Farta -= gamer.Score;
                    if (Farta <= 0)
                        dad = gamer.brain;
                }
                newgamers[i] = new Gamer(Brain.Cross(mom, dad));
            }
            TotalScore = 0;
            return newgamers;
        }
    }
}
