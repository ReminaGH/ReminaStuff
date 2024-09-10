#include "Map.h"
#include "ResourceManager.h"
#include "Physics.h"
#include "PickUp.h"
#include "Game.h"
#include "Enemy.h"

#include <box2d/b2_body.h>
#include <box2d/b2_fixture.h>
#include <box2d/b2_polygon_shape.h>
#include <iostream>

Map::Map(float cellSize)
	: cellSize(cellSize), grid()
{
}

void Map::CreaterCheckBoard(size_t width, size_t height)
{

	grid = std::vector(width, std::vector(height, (sf::Texture*)nullptr));

	bool lastV = 0;

	for (auto& column : grid)
	{

		for (auto& cell : column)
		{
				
			lastV = !lastV;
			if (lastV)
			{
				cell = &ResourceManager::textures["WallMario.png"];
			}
		}
		if (width % 2 == 0 )
		{

			lastV = !lastV;
		}

	}

}

void Map::Draw(Renderer& renderer)
{

	int x = 0;

	for (const auto& column : grid)
	{

		int y = 0;

		for (const auto& cell : column)
		{

			if (cell)
			{
					
				renderer.Draw(*cell,
					sf::Vector2f(cellSize * x + cellSize / 2.0f, cellSize * y + cellSize / 2.0f),
					sf::Vector2f(cellSize, cellSize));

			}
			y++;
		}
		x++;
	}

}

sf::Vector2f Map::CreateFromImage(const sf::Image& image, std::vector<Object*>& objects)
{
	objects.clear();
	grid.clear();
	grid = std::vector(image.getSize().x, std::vector(image.getSize().y, (sf::Texture*)nullptr));

	sf::Vector2f playerPos{};

	for (size_t x = 0; x < grid.size(); x++)
	{

		for (size_t y = 0; y < grid[x].size(); y++)
		{

			sf::Color color = image.getPixel(x, y);

			Object* object = nullptr;

			if (color == sf::Color::Green) //Player
			{
				playerPos = sf::Vector2f(cellSize * x + cellSize / 2.0f, cellSize * y + cellSize / 2.0f);
				continue;
			}
			else if (color == sf::Color::Black) //Blocks
			{
				grid[x][y] = &ResourceManager::textures["WallMario.png"];
			}
			else if (color == sf::Color::Blue) //Alt Blocks 1
			{
				grid[x][y] = &ResourceManager::textures["RoofMario.png"];
			}
			else if (color == sf::Color::Yellow) //Pickles
			{
				object = new PickUp();
			}
			else if (color == sf::Color::Red) //Enemy
			{
				object = new Enemy();
			}

			if (object)
			{

				object->position = sf::Vector2f(cellSize * x + cellSize / 2.0f, cellSize * y + cellSize / 2.0f);
				objects.push_back(object);

			}
			else if(grid[x][y])
			{

				b2BodyDef bodyDef{};
				bodyDef.position.Set(cellSize * x + cellSize / 2.0f, cellSize * y + cellSize / 2.0f);

				b2Body* body = Physics::world->CreateBody(&bodyDef);
				b2PolygonShape shape{};
				shape.SetAsBox(cellSize / 2.0f, cellSize / 2.0f);

				FixtureData* fixtureData = new FixtureData();
				fixtureData->type = FixtureDataType::MapTile;
				fixtureData->mapX = x;
				fixtureData->mapY = y;

				b2FixtureDef fixtureDef{};
				fixtureDef.userData.pointer = (uintptr_t)fixtureData;
				fixtureDef.density = 0.0f;
				fixtureDef.shape = &shape;
				body->CreateFixture(&fixtureDef);

			}

		}

	}

	return playerPos;
}
	