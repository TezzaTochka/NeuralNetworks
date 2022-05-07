#include "Matrix.h"
void Matrix::Init(int row, int col) {
	this->row = row;
	this->col = col;
	matrix = new double* [row];
	for (int i = 0; i < row; i++) {
		matrix[i] = new double[col];
		for (int j = 0; j < col; j++)
			matrix[i][j] = ((rand() % 100)) * 0.03 / (row + 35);
	}
}
void Matrix::Multi(const Matrix& m, const double* v, int n, double* z) {
	if (m.col != n)
		throw std::runtime_error("Error\n");
	for (int i = 0; i < m.row; i++) {
		double sum = 0;
		for (int j = 0; j < n; j++)
			sum += m.matrix[i][j] * v[j];
		z[i] = sum;
	}
}
void Matrix::Multi_T(const Matrix& m, const double* v, int n, double* z) {
	if (m.row != n)
		throw std::runtime_error("Error\n");
	for (int i = 0; i < m.col; i++) {
		double sum = 0;
		for (int j = 0; j < n; j++)
			sum += m.matrix[j][i] * v[j];
		z[i] = sum;
	}
}
void Matrix::Multi_V(double* z, int n, const double* v) {
	for (int i = 0; i < n; i++)
		z[i] *= v[i];
}
void Matrix::Sum(double* z, int n, const double* v) {
	for (int i = 0; i < n; i++)
		z[i] += v[i];
}
std::ostream& operator << (std::ostream& os, const Matrix& m) {
	for (int i = 0; i < m.row; i++)
		for (int j = 0; j < m.col; j++)
			os << m.matrix[i][j] << " ";
	return os;
}
