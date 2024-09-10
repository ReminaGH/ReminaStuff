#pragma once

#include "Animation.h"
#include "Object.h"
#include "Physics.h"

#include "SFML/Audio.hpp"

class Enemy
	: public Object
{

public:
	virtual void Begin() override;
	virtual void Render(Renderer& renderer) override;
	virtual void Update(float deltaTime) override;

	void Die();
	//bool isDead();

private:

	Animation animation{}, animation2{};
	float movement = 3.0f;

	FixtureData fixtureData{};
	b2Body* eBody{};

	float destroyTimer = 0.0f;
	bool isDead = false;

	sf::Sound changeNoise{};


};

