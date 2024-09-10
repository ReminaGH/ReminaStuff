#pragma once

#include "SFML/Graphics.hpp"

#include "Renderer.h"
#include "Camera.h"
#include "Object.h"
#include "PickUp.h"

extern Camera camera;

void Begin(const sf::Window& window);
void Update(float deltaTime);
void Render(Renderer& renderer);
void RenderUI(Renderer& renderer);

void DeleteObject(Object* object);
