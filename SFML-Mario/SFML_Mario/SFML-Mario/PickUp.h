#pragma once

#include "Object.h"
#include "Animation.h"
#include "ResourceManager.h"

#include <box2d/b2_body.h>

class PickUp :
	public Object
{	

public:

	~PickUp();

	virtual void Begin() override;
	virtual void Update(float deltaTima) override;
	virtual void Render(Renderer& renderer) override;

private:
	Animation animation;
	b2Body* body;
};

	