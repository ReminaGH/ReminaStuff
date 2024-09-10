#include "Player.h"
#include "ResourceManager.h"
#include "Object.h"
#include "Game.h"
#include "Enemy.h"

#include <vector>
#include "SFML/Graphics.hpp"
#include <box2d/b2_polygon_shape.h>
#include <box2d/b2_circle_shape.h>
#include <box2d/b2_fixture.h>

#include <iostream>
#include <random> 


const float moveSpeed = 7.0f;
const float jumpVelocity = 10.0f;

float randomFloat;

std::random_device rd;
std::mt19937 gen(rd());
std::uniform_real_distribution<float> dist(0.2f, 3.0f);

void Player::Begin()
{
	runAnim = Animation(0.6f,
	{
	
		AnimFrame(0.6f, ResourceManager::textures["HamburgerLegs2Walk7.png"]), 
		AnimFrame(0.5f, ResourceManager::textures["HamburgerLegs2Walk6.png"]),
		AnimFrame(0.4f, ResourceManager::textures["HamburgerLegs2Walk5.png"]),
		AnimFrame(0.3f, ResourceManager::textures["HamburgerLegs2Walk4.png"]),
		AnimFrame(0.2f, ResourceManager::textures["HamburgerLegs2Walk3.png"]),
		AnimFrame(0.1f, ResourceManager::textures["HamburgerLegs2Walk2.png"]),
		AnimFrame(0.0f, ResourceManager::textures["HamburgerLegs2Walk1.png"]),
	
	});

	sprintAnim = Animation(0.3f,
		{

			AnimFrame(0.3f, ResourceManager::textures["HamburgerLegs2Walk7.png"]),
			AnimFrame(0.25f, ResourceManager::textures["HamburgerLegs2Walk6.png"]),
			AnimFrame(0.2f, ResourceManager::textures["HamburgerLegs2Walk5.png"]),
			AnimFrame(0.15f, ResourceManager::textures["HamburgerLegs2Walk4.png"]),
			AnimFrame(0.1f, ResourceManager::textures["HamburgerLegs2Walk3.png"]),
			AnimFrame(0.05f, ResourceManager::textures["HamburgerLegs2Walk2.png"]),
			AnimFrame(0.0f, ResourceManager::textures["HamburgerLegs2Walk1.png"]),

		});

	jumpSound.setBuffer(ResourceManager::sounds["hurt_male.wav"]);
	jumpSound.setVolume(50);
	jumpSound.setPitch(3.0f);

	fixtureData.listener = this;
	fixtureData.player = this;
	fixtureData.type = FixtureDataType::Player;

	b2BodyDef bodyDef{};
	bodyDef.type = b2_dynamicBody;
	bodyDef.position.Set(position.x, position.y);
	bodyDef.fixedRotation = true;
	body = Physics::world->CreateBody(&bodyDef);

	b2FixtureDef fixtureDef{};
	fixtureDef.userData.pointer = (uintptr_t)&fixtureData;
	fixtureDef.density = 1.0f;
	fixtureDef.friction = 0.0f;

	b2CircleShape circleShape{};
	circleShape.m_radius = 0.5f;
	circleShape.m_p.Set(0.0f, -0.5f);
	fixtureDef.shape = &circleShape;
	body->CreateFixture(&fixtureDef);

	circleShape.m_p.Set(0.0f, 0.5f);
	body->CreateFixture(&fixtureDef);

	b2PolygonShape polyShape{};
	polyShape.SetAsBox(0.5f, 0.5f);
	fixtureDef.shape = &polyShape;
	body->CreateFixture(&fixtureDef);

	polyShape.SetAsBox(0.4f, 0.3f, b2Vec2(0.0f, 1.0f), 0.0f);
	fixtureDef.isSensor = true;
	groundFixture = body->CreateFixture(&fixtureDef);
}

void Player::Update(float deltaTime)
{
	std::srand(std::time(0));

	float moveSpeedAltered = moveSpeed;

	runAnim.Update(deltaTime);
	sprintAnim.Update(deltaTime);

	if (sf::Keyboard::isKeyPressed(sf::Keyboard::LShift))
	{
		moveSpeedAltered *= 2;
		isSprinting = true;
	}
	else
	{
		isSprinting = false;
	}

	b2Vec2 velocity = body->GetLinearVelocity();
	velocity.x = 0.0f;

	if (sf::Keyboard::isKeyPressed(sf::Keyboard::A) || sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
	{
		velocity.x -= moveSpeedAltered;
	}
	
	if (sf::Keyboard::isKeyPressed(sf::Keyboard::D) || sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
	{
		velocity.x += moveSpeedAltered;
	}
	
	if ((sf::Keyboard::isKeyPressed(sf::Keyboard::W) || sf::Keyboard::isKeyPressed(sf::Keyboard::Up)) && isGrounded)
	{
		velocity.y = -jumpVelocity;
		randomFloat = dist(gen);
		jumpSound.setPitch(randomFloat);
		jumpSound.play();
	}

	if (!isSprinting)
	{
		textureToDraw = runAnim.GetTexutre();
	} 
	else if (isSprinting)
	{
		textureToDraw = sprintAnim.GetTexutre();
	}

	if (velocity.x < -0.2f)
	{
		facingLeft = true;
	}
	else if (velocity.x > 0.02f)
	{
		facingLeft = false;
	} 
	else
	{
		textureToDraw = ResourceManager::textures["HamburgerLegs2Idle.png"];
	}

	if (!isGrounded)
	{
		textureToDraw = ResourceManager::textures["HamburgerLegs2Jump.png"];
	}

	body->SetLinearVelocity(velocity);

	position = sf::Vector2f(body->GetPosition().x, body->GetPosition().y);

	angle = body->GetAngle() * (180.0f / M_PI);


}

void Player::Draw(Renderer& renderer)
{

	renderer.Draw(textureToDraw, position, sf::Vector2f(facingLeft ? -2.0f : 2.0f, 2.0f), angle);

}

int Player::GetPickles() const
{
	return pickles;
}

void Player::BeginContact(b2Fixture* self, b2Fixture* other) 
{
	FixtureData* data = (FixtureData*)other->GetUserData().pointer;

	if (!data)
	{
		return;
	}

	if(groundFixture == self && data->type == FixtureDataType::MapTile)
	{
	isGrounded++;
	}
	else if (data->type == FixtureDataType::Object && data->object->tag == "pickle")
	{
		DeleteObject(data->object);
		std::cout << "Pickles : " << ++pickles << "\n";
	}
	
	else if (data->type == FixtureDataType::Object && data->object->tag == "enemy")
	{

		Enemy* enemy = dynamic_cast<Enemy*>(data->object);
		if (!enemy)
			return;

		if (groundFixture == self)
		{
			enemy->Die();
		}
		

	}
	
}

void Player::EndContact(b2Fixture* self, b2Fixture* other)
{
	FixtureData* data = (FixtureData*)other->GetUserData().pointer;

	if (!data)
	{
		return;
	}

	if (groundFixture == self && data->type == FixtureDataType::MapTile && isGrounded > 0)
	{
		isGrounded--;
	}
	
}
