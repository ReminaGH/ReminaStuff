#pragma once
#include "Renderer.h"

#include <box2d/b2_world.h>
#include <box2d/b2_fixture.h>
#include <box2d/b2_body.h>

#include <vector>
#include <unordered_set>

constexpr float M_PI = 22.0f / 7.0f;

class MyDebugDraw;
class Object;
class Player;

class ContactListener
{

public:
	virtual void BeginContact(b2Fixture* self, b2Fixture* other) = 0;
	virtual void EndContact(b2Fixture* self, b2Fixture* other) = 0;

};

enum class FixtureDataType
{

	Player,
	MapTile,
	Object

};

struct FixtureData
{

	FixtureDataType type;
	ContactListener* listener;

	union
	{

		Player* player;
		Object* object;
		struct { int mapX, mapY; };

	};
};


class Physics
{

public:
	static void Init();
	static void Update(float deltaTima);
	static void DebugDraw(Renderer& renderer);
	static void MarkBodyForDestruction(b2Body* body);


	static b2World* world;
	static MyDebugDraw* debugDraw;

	static std::unordered_set<b2Body*> bodiesToDestroy;
};


	