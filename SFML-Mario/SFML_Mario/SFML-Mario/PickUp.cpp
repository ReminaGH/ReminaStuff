#include "PickUp.h"
#include "Physics.h"

#include <box2d/b2_polygon_shape.h>
#include <box2d/b2_fixture.h>


PickUp::~PickUp()
{

	Physics::world->DestroyBody(body);

}

void PickUp::Begin()
{
	tag = "pickle";

	animation = Animation(4.0f,
		{

			AnimFrame(3.9f, ResourceManager::textures["LowPolyPickle (40).gif"]),
			AnimFrame(3.8f, ResourceManager::textures["LowPolyPickle (39).gif"]),
			AnimFrame(3.7f, ResourceManager::textures["LowPolyPickle (38).gif"]),
			AnimFrame(3.6f, ResourceManager::textures["LowPolyPickle (37).gif"]),
			AnimFrame(3.5f, ResourceManager::textures["LowPolyPickle (36).gif"]),
			AnimFrame(3.4f, ResourceManager::textures["LowPolyPickle (35).gif"]),
			AnimFrame(3.3f, ResourceManager::textures["LowPolyPickle (34).gif"]),
			AnimFrame(3.2f, ResourceManager::textures["LowPolyPickle (33).gif"]),
			AnimFrame(3.1f, ResourceManager::textures["LowPolyPickle (32).gif"]),
			AnimFrame(3.0f, ResourceManager::textures["LowPolyPickle (31).gif"]),
			AnimFrame(2.9f, ResourceManager::textures["LowPolyPickle (30).gif"]),
			AnimFrame(2.8f, ResourceManager::textures["LowPolyPickle (29).gif"]),
			AnimFrame(2.7f, ResourceManager::textures["LowPolyPickle (28).gif"]),
			AnimFrame(2.6f, ResourceManager::textures["LowPolyPickle (27).gif"]),
			AnimFrame(2.5f, ResourceManager::textures["LowPolyPickle (26).gif"]),
			AnimFrame(2.4f, ResourceManager::textures["LowPolyPickle (25).gif"]),
			AnimFrame(2.3f, ResourceManager::textures["LowPolyPickle (24).gif"]),
			AnimFrame(2.2f, ResourceManager::textures["LowPolyPickle (23).gif"]),
			AnimFrame(2.1f, ResourceManager::textures["LowPolyPickle (22).gif"]),
			AnimFrame(2.0f, ResourceManager::textures["LowPolyPickle (21).gif"]),
			AnimFrame(1.9f, ResourceManager::textures["LowPolyPickle (20).gif"]),
			AnimFrame(1.8f, ResourceManager::textures["LowPolyPickle (19).gif"]),
			AnimFrame(1.7f, ResourceManager::textures["LowPolyPickle (18).gif"]),
			AnimFrame(1.6f, ResourceManager::textures["LowPolyPickle (17).gif"]),
			AnimFrame(1.5f, ResourceManager::textures["LowPolyPickle (16).gif"]),
			AnimFrame(1.4f, ResourceManager::textures["LowPolyPickle (15).gif"]),
			AnimFrame(1.3f, ResourceManager::textures["LowPolyPickle (14).gif"]),	
			AnimFrame(1.2f, ResourceManager::textures["LowPolyPickle (13).gif"]),
			AnimFrame(1.1f, ResourceManager::textures["LowPolyPickle (12).gif"]),
			AnimFrame(1.0f, ResourceManager::textures["LowPolyPickle (11).gif"]),
			AnimFrame(0.9f, ResourceManager::textures["LowPolyPickle (10).gif"]),
			AnimFrame(0.8f, ResourceManager::textures["LowPolyPickle (9).gif"]),
			AnimFrame(0.7f, ResourceManager::textures["LowPolyPickle (8).gif"]),
			AnimFrame(0.6f, ResourceManager::textures["LowPolyPickle (7).gif"]),
			AnimFrame(0.5f, ResourceManager::textures["LowPolyPickle (6).gif"]),
			AnimFrame(0.4f, ResourceManager::textures["LowPolyPickle (5).gif"]),
			AnimFrame(0.3f, ResourceManager::textures["LowPolyPickle (4).gif"]),
			AnimFrame(0.2f, ResourceManager::textures["LowPolyPickle (3).gif"]),
			AnimFrame(0.1f, ResourceManager::textures["LowPolyPickle (2).gif"]),
			AnimFrame(0.0f, ResourceManager::textures["LowPolyPickle (1).gif"]),

		});

	b2BodyDef bodyDef{};
	bodyDef.position.Set(position.x, position.y);

	b2Body* body = Physics::world->CreateBody(&bodyDef);
	b2PolygonShape shape{};
	shape.SetAsBox(0.5f, 0.5f);

	FixtureData* fixtureData = new FixtureData();
	fixtureData->type = FixtureDataType::Object;
	fixtureData->object = this;

	b2FixtureDef fixtureDef{};
	fixtureDef.userData.pointer = (uintptr_t)fixtureData;
	fixtureDef.isSensor = true;
	fixtureDef.density = 0.0f;
	fixtureDef.shape = &shape;
	body->CreateFixture(&fixtureDef);

}

void PickUp::Update(float deltaTima)
{
	animation.Update(deltaTima);
}

void PickUp::Render(Renderer& renderer)
{
	renderer.Draw(animation.GetTexutre(), position, sf::Vector2f(1.0f, 1.0f));
}


