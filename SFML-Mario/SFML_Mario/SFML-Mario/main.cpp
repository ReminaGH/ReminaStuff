#include "SFML/Graphics.hpp"

#include "Game.h"
#include "Camera.h"
#include "Renderer.h"
#include "PickUp.h"

#include <iostream>
#include <windows.h> 

int main()
{

	sf::RenderWindow window(sf::VideoMode(1200, 900), "SFML-Mario");
	sf::Clock deltaClock;
	Renderer renderer(window);


	window.setFramerateLimit(60);

		
	Begin(window);

	while (window.isOpen())
	{

		float deltaTime = deltaClock.restart().asSeconds();

		sf::Event event{};
			
		while (window.pollEvent(event))
		{

			if (event.type == sf::Event::Closed)
			{

				window.close();

			}

		}
		Update(deltaTime);
		
		window.clear();

		window.setView(camera.GetView(window.getSize()));

		Render(renderer);

		window.setView(camera.GetUIView());
	
		RenderUI(renderer);

		window.display();
	}

}
