#include <iostream>
#include <SFML/Graphics.hpp>
#include "NetWork.h"
using namespace std;
using namespace sf;

class Data {
public:
	int digit;
	double* pix;
};
int main()
{
	NetWork NW{};
	ifstream fin;
	fin.open("weights.txt");
	if (!fin.is_open())
		throw runtime_error("Error\n");
	for (int i = 0; i < NW.l - 1; i++)
		fin >> NW.weights[i];
	for (int i = 0; i < NW.l - 1; i++)
		for (int j = 0; j < NW.size[i + 1]; j++)
			fin >> NW.bias[i][j];
	fin.close();
	int block_size = 25;
	int width_fornum = 28;
	int height_fornum = 28;
	int width_forinit = 150;
	RenderWindow window(VideoMode(width_fornum * block_size + width_forinit, height_fornum * block_size), "SFMLworks");
	RectangleShape winForInit(Vector2f(width_forinit, height_fornum * block_size));
	winForInit.setFillColor(Color(255, 255, 255));
	winForInit.setPosition(width_fornum * block_size, 0);
	double* pix = new double[784];
	for (int i = 0; i < 784; i++)
		pix[i] = 0;
	double color;
	int pred;
	double* preds = new double[10];
	for (int i = 0; i < 10; i++)
		preds[i] = 0;
	RectangleShape rect(Vector2f(0, 0));
	int x = 0;
	int y = 0;
	bool permitLeft = false;
	bool permitRight = false;
	Font font;
	if (!font.loadFromFile("arial.ttf"))
		throw runtime_error("Error\n");
	Text txt;
	txt.setFont(font);
	txt.setCharacterSize(24);
	txt.setFillColor(Color::Black);
	while (window.isOpen()) {
		Event event;
		while (window.pollEvent(event))
		{
			if (event.type == Event::Closed)
				window.close();
			if (event.type == Event::MouseMoved) {
				x = event.mouseMove.x / block_size;
				y = event.mouseMove.y / block_size;
			}
			if ((x < 0 || x >= width_fornum) || (y < 0 || y >= height_fornum) || (event.type == Event::MouseButtonReleased)) {
				permitLeft = false;
				permitRight = false;
			}
			if ((event.type == Event::MouseButtonPressed && event.mouseButton.button == Mouse::Left) || permitLeft) {
				permitLeft = true;
				pix[y * width_fornum + x] += 0.5;
				if (pix[y * width_fornum + x] >= 1)
					pix[y * width_fornum + x] = 1;
			}
			if ((event.type == Event::MouseButtonPressed && event.mouseButton.button == Mouse::Right) || permitRight) {
				permitRight = true;
				pix[y * width_fornum + x] -= 0.5;
				if (pix[y * width_fornum + x] <= 0)
					pix[y * width_fornum + x] = 0;
			}
		}
		NW.Set(pix);
		pred = NW.Forward();
		NW.Res(preds);
		window.clear();
		window.draw(winForInit);
		for (int i = 0; i < height_fornum; i++)
			for (int j = 0; j < width_fornum; j++) {
				rect.setSize(Vector2f(block_size, block_size));
				color = 255 * pix[i * height_fornum + j];
				rect.setFillColor(Color(color, color, color));
				rect.setPosition(j * block_size, i * block_size);
				window.draw(rect);
			}
		for (int i = 0; i < 10; i++) {
			txt.setString(to_string(i));
			txt.setPosition(width_fornum * block_size + 5, 60 * i + 50);
			window.draw(txt);
			rect.setSize(Vector2f(90 * preds[i], 10));
			rect.setFillColor(Color::Red);
			rect.setPosition(width_fornum * block_size + 30, 60 * i + 60);
			window.draw(rect);
		}
		window.display();
	}
	return 0;
}
