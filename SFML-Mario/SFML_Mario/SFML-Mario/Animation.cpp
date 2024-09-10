#include "Animation.h"



Animation::Animation(float length, std::vector<AnimFrame> frames)
	: length(length), frames(frames)
{


}

void Animation::Update(float deltaTime)
{

	time += deltaTime;

}

sf::Texture Animation::GetTexutre()
{
	while (time > length)
	{

		time -= length;

	}

	for (const auto& frame : frames)
	{

		if (time >= frame.time)
		{

			return frame.texture;

		}

	}
	return sf::Texture();
}
