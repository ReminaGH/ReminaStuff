#pragma once

#include <SFML/Graphics.hpp>
#include <vector>

struct AnimFrame
{
	AnimFrame(float time = 0.0f, sf::Texture texture = sf::Texture())
		: time(time), texture(texture)
	{

	}

	float time = 0.0f;
	sf::Texture texture{};

};

class Animation
{

public:

	Animation(float length = 0.0f, std::vector<AnimFrame> frames = {});
	void Update(float deltaTime);

	sf::Texture GetTexutre();

private:
	float time = 0.0f;
	float length;
	std::vector<AnimFrame> frames;

};