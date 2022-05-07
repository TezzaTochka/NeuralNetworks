using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetworkPong
{
    public partial class Form1 : Form
    {
        static float[,] InputToF = { { -0.1119202f, 0.8517392f, -0.9397235f, 0.9654832f }, { -0.8493931f, -0.9024046f, -0.5389975f, -0.3350416f }, { -0.4417992f, 0.2953588f, 0.104504f, -0.2855988f } };
        static float[,] FToG = { { -0.1682196f, -0.02494727f, -0.4439248f, 0.1676177f }, { 0.56344f, 0.6927354f, 0.5343848f, -0.003594683f }, { 0.1459742f, 0.01463396f, -0.406444f, -0.4420252f } };
        static float[,] GToOutput = { { -0.7780038f, 0.8324569f, 0.45364f, -0.2343332f }, { 0.3288026f, 0.2643575f, 0.477881f, 0.02748516f } };
        static Brain brain = new Brain(new Matrix(InputToF), new Matrix(FToG), new Matrix(GToOutput));
        Gamer gamer = new Gamer(brain);
        int FrameWork = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < FrameWork; i++)
            {
                gamer.Step();
                if (!gamer.IsAlive)
                    gamer = new Gamer(brain);
            }   
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gamer.Draw(e.Graphics);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '[')
                FrameWork = Math.Max(1, FrameWork - 1);
            else if (e.KeyChar == ']')
                FrameWork += 1;
        }
    }
}
