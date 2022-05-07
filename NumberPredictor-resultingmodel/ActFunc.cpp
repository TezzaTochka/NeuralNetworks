#include "ActFunc.h"
void ActFunc::Use(double* z, int n) {
	for (int i = 0; i < n; i++)
		if (z[i] < 0)
			z[i] *= 0.01;
		else if (z[i] > 1)
			z[i] = 1. + 0.01 * (z[i] - 1.);
}
double ActFunc::Der(double v) {
	if (v < 0 || v > 1)
		v = 0.01;
	return v;
}
