#include "Enemy.h"
#include "ResourceManager.h"
#include "Game.h"

#include <box2d/b2_circle_shape.h>


void Enemy::Begin()
{

	changeNoise.setBuffer(ResourceManager::sounds["Ouh.ogg"]);
	changeNoise.setVolume(50);

	animation2 = Animation(1.7f,
		{
			AnimFrame(1.6f, ResourceManager::textures["explo1.png"]),
			AnimFrame(1.5f, ResourceManager::textures["explo2.png"]),
			AnimFrame(1.4f, ResourceManager::textures["explo3.png"]),
			AnimFrame(1.3f, ResourceManager::textures["explo4.png"]),
			AnimFrame(1.2f, ResourceManager::textures["explo5.png"]),
			AnimFrame(1.1f, ResourceManager::textures["explo6.png"]),
			AnimFrame(1.0f, ResourceManager::textures["explo7.png"]),
			AnimFrame(0.9f, ResourceManager::textures["explo8.png"]),
			AnimFrame(0.8f, ResourceManager::textures["explo9.png"]),
			AnimFrame(0.7f, ResourceManager::textures["explo10.png"]),
			AnimFrame(0.6f, ResourceManager::textures["explo11.png"]),
			AnimFrame(0.5f, ResourceManager::textures["explo12.png"]),
			AnimFrame(0.4f, ResourceManager::textures["explo13.png"]),
			AnimFrame(0.3f, ResourceManager::textures["explo15.png"]),
			AnimFrame(0.2f, ResourceManager::textures["explo16.png"]),
			AnimFrame(0.1f, ResourceManager::textures["explo17.png"]),
			AnimFrame(0.1f, ResourceManager::textures["explo1.png"]),


		});

	animation = Animation(0.4f,
		{
			AnimFrame(0.35f, ResourceManager::textures["clown1.png"]),
			AnimFrame(0.3f, ResourceManager::textures["clown2.png"]),
			AnimFrame(0.25f, ResourceManager::textures["clown3.png"]),
			AnimFrame(0.2f, ResourceManager::textures["clown4.png"]),
			AnimFrame(0.15f, ResourceManager::textures["clown5.png"]),
			AnimFrame(0.1f, ResourceManager::textures["clown6.png"]),
			AnimFrame(0.5f, ResourceManager::textures["clown7.png"]),
			AnimFrame(0.0f, ResourceManager::textures["clown8.png"]),

		});

		
	tag = "enemy";

	fixtureData.object = this;
	fixtureData.type = FixtureDataType::Object;

	b2BodyDef bodyDef{};
	bodyDef.type = b2_dynamicBody;
	bodyDef.position.Set(position.x, position.y);
	bodyDef.fixedRotation = true;
	eBody = Physics::world->CreateBody(&bodyDef);

	b2CircleShape circleShape{};
	circleShape.m_radius = 0.5f;
	
	b2FixtureDef fixtureDef{};
	fixtureDef.userData.pointer = (uintptr_t)&fixtureData;
	fixtureDef.shape = &circleShape;
	fixtureDef.density = 1.0f;
	fixtureDef.friction = 0.0f;
	eBody->CreateFixture(&fixtureDef);

}

void Enemy::Render(Renderer& renderer)
{

			renderer.Draw(animation.GetTexutre(), !isDead ? position : sf::Vector2f(position.x, position.y + 0.45f), sf::Vector2f(1.5f, isDead ? 0.2f : 1.5f), angle);
		
	
}

void Enemy::Update(float deltaTime)
{
	if (isDead)
	{
		destroyTimer += deltaTime;
		if (destroyTimer >= 2.0f)
		{
			DeleteObject(this);
			
			return;
		}
	}

	animation.Update(deltaTime);

	b2Vec2 velocity = eBody->GetLinearVelocity();

	if (std::abs(velocity.x) <= 0.02f)
	{
		movement *= -1.0f;
		changeNoise.play();
	}

	velocity.x = movement;

	eBody->SetLinearVelocity(velocity);

	position = sf::Vector2f(eBody->GetPosition().x, eBody->GetPosition().y);
	angle = eBody->GetAngle() * (180 / M_PI);
}

void Enemy::Die()
{
	isDead = true;


	//The current solution to fix the problem, changing the game to debugg will result in a different result in debugg
	//while also giving an error for mananging the sound,but thats beside the issue
	Physics::MarkBodyForDestruction(eBody);
	
	
}
