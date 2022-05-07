#include <iostream>
#include "NetWork.h"
using namespace std;

class Data {
public:
	int digit;
	double* pix;
};
int main()
{
	cout << "Wait a little time\n";
	NetWork NW{};
	int size;
	int size1;
	int size2;
	Data* nums;
	ifstream fin;
	fin.open("lib_MNIST_first.txt");
	if (!fin.is_open())
		throw runtime_error("Error\n");
	ifstream fin2;
	fin2.open("lib_MNIST_second.txt");
	if (!fin2.is_open())
		throw runtime_error("Error\n");
	fin >> size1;
	fin2 >> size2;
	size = size1 + size2;
	nums = new Data[size];
	for (int i = 0; i < size1; i++) {
		fin >> nums[i].digit;
		nums[i].pix = new double[NW.size[0]];
		for (int j = 0; j < NW.size[0]; j++)
			fin >> nums[i].pix[j];
	}
	for (int i = size1; i < size; i++) {
		fin2 >> nums[i].digit;
		nums[i].pix = new double[NW.size[0]];
		for (int j = 0; j < NW.size[0]; j++)
			fin2 >> nums[i].pix[j];
	}
	fin.close();
	fin2.close();
	cout << "Learning starting\n";

	int epoch = 0;
	while (true) {
		int treb;
		int pred;
		double ra = 0;
		for (int i = 0; i < size; i++) {
			NW.ShutDown(0.2);
			NW.Set(nums[i].pix);
			treb = nums[i].digit;
			pred = NW.Forward();
			if (pred != treb) {
				NW.BackPropogation(treb);
				NW.Update(0.15 * exp(-epoch / 20.));
			}
			else
				ra++;
		}
		cout << "Epoch: " << epoch + 1 << " " << "Right answers: " << ra / size * 100 << "%" << endl;
		epoch++;
		if (epoch == 20)
			break;
	}
	cout << "The weights of trained neural network are recording\n";
	ofstream fout;
	fout.open("weights.txt");
	if (!fout.is_open())
		throw runtime_error("Error\n");
	for (int i = 0; i < NW.l - 1; i++)
		fout << NW.weights[i] << " ";
	for (int i = 0; i < NW.l - 1; i++)
		for (int j = 0; j < NW.size[i + 1]; j++)
			fout << NW.bias[i][j] << " ";
	fout.close();
	cout << "Wait a little time\n";
	Data* nums2;
	fin.open("lib_10k.txt");
	if (!fin.is_open())
		throw runtime_error("Error\n");
	fin >> size2;
	nums2 = new Data[size2];
	for (int i = 0; i < size2; i++) {
		fin >> nums2[i].digit;
		nums2[i].pix = new double[NW.size[0]];
		for (int j = 0; j < NW.size[0]; j++)
			fin >> nums2[i].pix[j];
	}
	fin.close();
	cout << "Testing starting\n";
	double ra = 0;
	int treb;
	int pred;
	NW.ShutDown(-1.);
	for (int i = 0; i < size2; i++) {
		NW.Set(nums2[i].pix);
		treb = nums2[i].digit;
		pred = NW.Forward();
		if (pred == treb)
			ra++;
	}
	cout << "Right answers: " << ra / size2 * 100 << "%" << endl;
	return 0;
}
