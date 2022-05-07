using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkSnake
{
    static class Extenshions
    {
        public static Matrix AttachOne(Matrix a)
        {
            float[,] cells = new float[a.Rows + 1, 1];
            for (int i = 0; i < a.Rows; i++)
                cells[i, 0] = a[i, 0];
            cells[a.Rows, 0] = 1;
            return new Matrix(cells);
        }
        public static Matrix VectorToColumnMatrix(List<float> list)
        {
            float[,] cells = new float[list.Count(), 1];
            for (int i = 0; i < list.Count(); i++)
                cells[i, 0] = list[i];
            return new Matrix(cells);
        }
        public static float[] ColumnMatrixToRow(Matrix a)
        {
            float[] cells = new float[a.Rows];
            for (int i = 0; i < a.Rows; i++)
                cells[i] = a[i, 0];
            return cells;
        }
        public static Matrix Relu(Matrix a)
        {
            float[,] cells = new float[a.Rows, 1];
            for (int i = 0; i < a.Rows; i++)
                cells[i, 0] = Math.Max(0, a[i, 0]);
            return new Matrix(cells);
        }
        public static int ArgMax(float[] a)
        {
            int maxindex = 0;
            for (int i = 1; i < Brain.Output_Dim; i++)
                if (a[i] > a[maxindex])
                    maxindex = i;
            return maxindex;
        }
    }
}
