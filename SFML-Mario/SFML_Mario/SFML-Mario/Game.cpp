#include "Game.h"
#include "ResourceManager.h"
#include "Map.h"
#include "Player.h"
#include "Physics.h"

#include <SFML/Audio.hpp>
#include <filesystem>


Map map(1.0f);
Camera camera(20.0f);
Player player{};
std::vector<Object*> objects{};

sf::Music music{};

sf::Font font{};
sf::Text pickleCount("Pickles: ", font);

Animation pickle_anim;


void Begin(const sf::Window& window)
{

	for (auto& file : std::filesystem::directory_iterator("./resources/textures/"))
	{

		if (file.is_regular_file() && (file.path().extension() == ".png") || (file.path().extension() == ".jpg") || (file.path().extension() == ".gif"))
		{

			ResourceManager::textures[file.path().filename().string()].loadFromFile(file.path().string());

		}

	}

	for (auto& file : std::filesystem::directory_iterator("./resources/sounds/")) // doesn't quite work
	{

		if (file.is_regular_file() && (file.path().extension() == ".ogg") || (file.path().extension() == ".wav"))
		{

			ResourceManager::sounds[file.path().filename().string()].loadFromFile(file.path().string());

		}

	}

	music.openFromFile(".\\resources\\sounds\\music.ogg");
	music.setLoop(true);
	music.setVolume(20);

	font.loadFromFile(".\\resources\\PixelifySans.ttf");
	pickleCount.setFillColor(sf::Color::White);
	pickleCount.setOutlineColor(sf::Color::Black);
	pickleCount.setOutlineThickness(1.0f);
	pickleCount.setScale(0.1f, 0.1f);

	Physics::Init();

	sf::Image image{};
	image.loadFromFile(".\\resources\\map.png");
	player.position = map.CreateFromImage(image, objects);
	player.Begin();

	for (auto& object : objects)
	{
		object->Begin();
	}

	music.play();

}

void Update(float deltaTime)
{
	Physics::Update(deltaTime);
	player.Update(deltaTime);
	camera.position = player.position;

	for (auto& object : objects)
	{
		object->Update(deltaTime);
	}

}

void Render(Renderer& renderer)
{

	renderer.Draw(ResourceManager::textures["background.png"], camera.position, camera.GetViewSize());

	map.Draw(renderer);

	for (auto& object : objects)
	{
		object->Render(renderer);
	}

	

	player.Draw(renderer);
	Physics::DebugDraw(renderer);
	

}

void RenderUI(Renderer& renderer)
{
	pickleCount.setPosition(-camera.GetViewSize() / 2.0f + sf::Vector2f(2.0f, 1.0f));
	pickleCount.setString("Pickles: " + std::to_string(player.GetPickles()));
	renderer.target.draw(pickleCount);
	renderer.Draw(ResourceManager::textures["LowPolyPickle (1).gif"], -camera.GetViewSize() / 2.0f + sf::Vector2f(18.0f, 3.0f), sf::Vector2f(5.0f, 5.0f));


}

void DeleteObject(Object* object)
{

	const auto& it = std::find(objects.begin(), objects.end(), object);
	if (it != objects.end())
	{
		delete *it;
		objects.erase(it);
	}

}

