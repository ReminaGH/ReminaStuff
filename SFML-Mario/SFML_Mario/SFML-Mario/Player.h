#pragma once

#include "SFML/Audio.hpp"
#include "SFML/Graphics.hpp"
#include "box2d/b2_body.h"
#include "Renderer.h"
#include "Physics.h"
#include "Animation.h"


class Player
	: public ContactListener
{

public: 
	void Begin();
	void Update(float deltaTime);
	void Draw(Renderer& renderer);
	int GetPickles() const;

	virtual void BeginContact(b2Fixture* self, b2Fixture* other) override;
	virtual void EndContact(b2Fixture* self, b2Fixture* other) override;

	sf::Vector2f position{};
	float angle{};
		
private:
	FixtureData fixtureData{};
	b2Body* body{};
	b2Fixture* groundFixture{};

	Animation runAnim{};
	Animation sprintAnim{};
	sf::Texture textureToDraw{};
	sf::Sound jumpSound{};

	size_t isGrounded = 0;
	bool facingLeft = false;
	bool isSprinting = false;

	size_t pickles{};
};

