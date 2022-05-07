#include "NetWork.h"
NetWork::NetWork() {
	srand(time(NULL));
	l = 3;
	size = new int[l];
	size[0] = 784; size[1] = 256; size[2] = 10;
	weights = new Matrix[l - 1];
	bias = new double* [l - 1];
	for (int i = 0; i < l - 1; i++) {
		weights[i].Init(size[i + 1], size[i]);
		bias[i] = new double[size[i + 1]];
		for (int j = 0; j < size[i + 1]; j++)
			bias[i][j] = ((rand() % 50)) * 0.06 / (size[i] + 15);
	}
	neurons = new double* [l];
	neurons_err = new double* [l];
	neurons_off = new double* [l];
	for (int i = 0; i < l; i++) {
		neurons[i] = new double[size[i]];
		neurons_err[i] = new double[size[i]];
		neurons_off[i] = new double[size[i]];
	}
	for (int i = 0; i < l; i++)
		for (int j = 0; j < size[i]; j++)
			neurons_off[i][j] = 1.;
}
void NetWork::ShutDown(double chance) {
	srand(time(NULL));
	for (int i = 1; i < l - 1; i++) {
		int k = 0;
		for (int j = 0; j < size[i]; j++) {
			if ((rand() % 1001) / 1000. > chance)
				neurons_off[i][j] = 1.;
			else {
				neurons_off[i][j] = 0.;
				k++;
			}
		}
		if (k == size[i])
			neurons_off[i][rand() % size[i]] = 1.;
	}
}
void NetWork::Set(double* v) {
	for (int i = 0; i < size[0]; i++)
		neurons[0][i] = v[i];
}
int NetWork::Forward() {
	for (int i = 0; i < l - 1; i++) {
		Matrix::Multi(weights[i], neurons[i], size[i], neurons[i + 1]);
		Matrix::Sum(neurons[i + 1], size[i + 1], bias[i]);
		ActFunc::Use(neurons[i + 1], size[i + 1]);
		Matrix::Multi_V(neurons[i + 1], size[i + 1], neurons_off[i + 1]);
	}
	int maxind = 0;
	for (int i = 1; i < size[l - 1]; i++)
		if (neurons[l - 1][i] > neurons[l - 1][maxind])
			maxind = i;
	return maxind;
}
void NetWork::BackPropogation(int num) {
	for (int i = 0; i < size[l - 1]; i++)
		if (i != num)
			neurons_err[l - 1][i] = -neurons[l - 1][i] * ActFunc::Der(neurons[l - 1][i]);
		else
			neurons_err[l - 1][i] = (1. - neurons[l - 1][i]) * ActFunc::Der(neurons[l - 1][i]);
	for (int i = l - 2; i > 0; i--) {
		Matrix::Multi_T(weights[i], neurons_err[i + 1], size[i + 1], neurons_err[i]);
		for (int j = 0; j < size[i]; j++)
			neurons_err[i][j] *= ActFunc::Der(neurons[i][j]);
		Matrix::Multi_V(neurons_err[i], size[i], neurons_off[i]);
	}
}
void NetWork::Update(double ind) {
	for (int i = 0; i < l - 1; i++)
		for (int j = 0; j < size[i + 1]; j++)
			for (int k = 0; k < size[i]; k++)
				weights[i].matrix[j][k] += neurons[i][k] * neurons_err[i + 1][j] * ind;
	for (int i = 0; i < l - 1; i++)
		for (int j = 0; j < size[i + 1]; j++)
			bias[i][j] += neurons_err[i + 1][j] * ind;
}
