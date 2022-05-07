using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkSnake
{
    class Brain
    {
        public const int Input_Dim = 24;
        public const int F_Dim = 18;
        public const int G_Dim = 18;
        public const int Output_Dim = 4;

        public Matrix InToF;
        public Matrix FToG;
        public Matrix GToOut;
        public Brain()
        {
            InToF = Matrix.Random(F_Dim, Input_Dim + 1);
            FToG = Matrix.Random(G_Dim, F_Dim + 1);
            GToOut = Matrix.Random(Output_Dim, G_Dim + 1);
        }
        public static Brain Cross(Brain mom, Brain dad)
        {
            Brain child = new Brain();
            child.InToF = Matrix.Cross(mom.InToF, dad.InToF);
            child.FToG = Matrix.Cross(mom.FToG, dad.FToG);
            child.GToOut = Matrix.Cross(mom.GToOut, dad.GToOut);
            return child;
        }
        public float[] Think(List<float> list)
        {
            Matrix t1 = Extenshions.Relu(InToF * Extenshions.AttachOne(Extenshions.VectorToColumnMatrix(list)));
            Matrix t2 = Extenshions.Relu(FToG * Extenshions.AttachOne(t1));
            float[] t3 = Extenshions.ColumnMatrixToRow(GToOut * Extenshions.AttachOne(t2));
            return t3;
        }
    }
}