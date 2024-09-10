#pragma once

#include "Renderer.h"
#include "SFML/Graphics.hpp"
#include "Object.h"

#include <vector>


class Map
{

public:

	Map(float cellSize = 32.0f);

	void CreaterCheckBoard(size_t width, size_t height);
	void Draw(Renderer& renderer);
	sf::Vector2f CreateFromImage(const sf::Image& image, std::vector<Object*>& objects);

	std::vector<std::vector<sf::Texture*>> grid;
	float cellSize;

};

