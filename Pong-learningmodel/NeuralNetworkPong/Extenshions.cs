using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    static class Extenshions
    {
        public static Matrix AttachOne(Matrix matrix)
        {
            float[,] cells = new float[matrix.Rows + 1, 1];
            for (int i = 0; i < matrix.Rows; i++)
                cells[i, 0] = matrix[i, 0];
            cells[matrix.Rows, 0] = 1f;
            return new Matrix(cells);
        }
        public static Matrix VectorToColumnMatrix(float[] vector)
        {
            float[,] cells = new float[vector.Length, 1];
            for (int i = 0; i < vector.Length; i++)
                cells[i, 0] = vector[i];
            return new Matrix(cells);
        }
        public static float[] ColumnMatrixToVector(Matrix matrix)
        {
            float[] vector = new float[matrix.Rows];
            for (int i = 0; i < matrix.Rows; i++)
                vector[i] = matrix[i, 0];
            return vector;
        }
        public static Matrix Relu(Matrix matrix)
        {
            float[,] cells = new float[matrix.Rows, 1];
            for (int i = 0; i < matrix.Rows; i++)
                cells[i, 0] = Math.Max(0, matrix[i, 0]);
            return new Matrix(cells);
        }
    }
}
