#pragma once
#include <iostream>
class Matrix
{
public:
	int row, col;
	double** matrix;
	void Init(int row, int col);
	static void Multi(const Matrix& m, const double* v, int n, double* z);
	static void Multi_T(const Matrix& m, const double* v, int n, double* z);
	static void Multi_V(double* z, int n, const double* v);
	static void Sum(double* z, int n, const double* v);
	friend std::istream& operator >> (std::istream& is, Matrix& m);
};
