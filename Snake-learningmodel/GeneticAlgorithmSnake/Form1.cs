using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetworkSnake
{
    public partial class Form1 : Form
    {
        Game game = new Game();
        float interval = 1f;
        public Form1()
        {
            InitializeComponent();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            game.Step();
            Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ']')
                game.TimeFrame += 1;
            else if (e.KeyChar == '[')
            {
                game.TimeFrame -= 1;
                if (game.TimeFrame < 1)
                    game.TimeFrame = 1;
            }
            else if (e.KeyChar == '+' || e.KeyChar == '=')
                Faster();
            else if (e.KeyChar == '-')
                Slower();
            else if (e.KeyChar == '>')
                game.trebs += 50;
            else if (e.KeyChar == '<')
                game.trebs -= 50;
        }
        private void Faster()
        {
            interval = (int)Math.Max(1, interval * 0.8);
            timer.Interval = (int)interval;
        }
        private void Slower()
        {
            interval = Math.Min(1000, interval / 0.5f);
            timer.Interval = (int)interval;
        }
    }
}
