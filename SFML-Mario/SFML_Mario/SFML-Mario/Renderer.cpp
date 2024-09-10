#include "Renderer.h"

#include <vector>

Renderer::Renderer(sf::RenderTarget& target)
	: target(target)
{
}

void Renderer::Draw(const sf::Texture& texutre, const sf::Vector2f& position, const sf::Vector2f& size, float angle)
{

	sprite.setTexture(texutre, true);
	sprite.setOrigin((sf::Vector2f)texutre.getSize() / 2.0f);
	sprite.setRotation(angle);
	sprite.setPosition(position);
	sprite.setScale(sf::Vector2f(size.x / texutre.getSize().x, size.y / texutre.getSize().y));

	target.draw(sprite);


}