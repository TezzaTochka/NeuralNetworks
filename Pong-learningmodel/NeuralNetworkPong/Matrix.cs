using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkPong
{
    class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }
        public float[,] Cells { get; }
        public Matrix(float[,] cells)
        {
            Rows = cells.GetLength(0);
            Columns = cells.GetLength(1);
            Cells = cells;
        }
        public float this[int x, int y]
        {
            get { return Cells[x, y]; }
            set { Cells[x, y] = value; }
        }
        public static Matrix Random(int rows, int columns)
        {
            float[,] cells = new float[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    cells[i, j] = Rnd.Getweight();
            return new Matrix(cells);
        }
        public static Matrix Cross(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new InvalidOperationException();
            float[,] cells = new float[a.Rows, a.Columns];
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                {
                    if (Rnd.GetFloat() < 0.5)
                        cells[i, j] = a[i, j];
                    else
                        cells[i, j] = b[i, j];
                    if (Rnd.GetFloat() < 0.01)
                        cells[i, j] = Math.Max(-1, Math.Min(1, cells[i, j] + Rnd.Getweight()));
                }
            return new Matrix(cells);
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
                throw new InvalidOperationException();
            float[,] cells = new float[a.Rows, b.Columns];
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < b.Columns; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < a.Columns; k++)
                        sum += a[i, k] * b[k, j];
                    cells[i, j] = sum;
                }
            return new Matrix(cells);
        }
    }
}
