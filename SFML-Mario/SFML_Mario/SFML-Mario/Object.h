#pragma once

#include "Renderer.h"

#include <iostream>

#include <box2d/b2_body.h>

class Object
{

public:
	virtual void Begin() {}
	virtual void Render(Renderer& renderer) {}
	virtual void Update(float deltaTime) {}	

	std::string tag{};

	sf::Vector2f position{};
	float angle{};
};

