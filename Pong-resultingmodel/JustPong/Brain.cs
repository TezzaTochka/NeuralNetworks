using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    class Brain
    {
        public const int Input_Dim = 3;
        public const int F_Dim = 3;
        public const int G_Dim = 3;
        public const int Output_Dim = 2;

        public Matrix InputToF;
        public Matrix FToG;
        public Matrix GToOutput;
        public Brain()
        {
            InputToF = Matrix.Random(F_Dim, Input_Dim + 1);
            FToG = Matrix.Random(G_Dim, F_Dim + 1);
            GToOutput = Matrix.Random(Output_Dim, G_Dim + 1);
        }
        public Brain(Matrix InputToF, Matrix FToG, Matrix GToOutput)
        {
            this.InputToF = InputToF;
            this.FToG = FToG;
            this.GToOutput = GToOutput;
        }
        public static Brain Cross(Brain mom, Brain dad) =>
            new Brain(Matrix.Cross(mom.InputToF, dad.InputToF), Matrix.Cross(mom.FToG, dad.FToG), Matrix.Cross(mom.GToOutput, dad.GToOutput));
        public float[] Think(float[] data)
        {
            Matrix t1 = Extenshions.Relu(InputToF * Extenshions.AttachOne(Extenshions.VectorToColumnMatrix(data)));
            Matrix t2 = Extenshions.Relu(FToG * Extenshions.AttachOne(t1));
            float[] result = Extenshions.ColumnMatrixToVector(GToOutput * Extenshions.AttachOne(t2));
            return result;
            //Extenshions.ColumnMatrixToVector(FToOutput * Extenshions.AttachOne(Extenshions.Relu(InputToF * Extenshions.AttachOne(Extenshions.VectorToColumnMatrix(data)))));
        }
    }
}
