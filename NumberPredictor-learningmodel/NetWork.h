#pragma once
#include <iostream>
#include "fstream"
#include "Matrix.h"
#include "ActFunc.h"
using namespace std;
class NetWork
{
public:
	int l;
	int* size;
	Matrix* weights;
	double** neurons, ** neurons_err, ** neurons_off;
	double** bias;
	NetWork();
	void ShutDown(double chance);
	void Set(double* v);
	int Forward();
	void BackPropogation(int num);
	void Update(double ind);
	void SaveWeights();
	void ReadWeights();
};

