#pragma once

#include "SFML/Graphics.hpp"

class Renderer
{

public:
	sf::RenderTarget& target;
	
	Renderer(sf::RenderTarget& target);
	
	void Draw(const sf::Texture& texutre, const sf::Vector2f& position, const sf::Vector2f& size, float angle = 0.0f);

private:
	sf::Sprite sprite{};

};

