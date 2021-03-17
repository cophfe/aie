using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mlib;

using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
	class PhysicsObject : GameObject
	{
		Collider collider;

		RLVector2 position;
		RLVector2 velocity;
		RLVector2 acceleration;
		float angularVelocity;

		float restitution;
		float drag = 0f;
		float mass;
		float iMass;
		float inetia;
		float iInetia;

		float gravity;


		public PhysicsObject(string fileName, Vector2 position, Collider collider = null, float drag = 0, float restitution = 0, float rotation = 0, float scale = 1, GameObject parent = null) : base(fileName, position, rotation, scale, parent)
		{
			this.collider = collider;
			this.restitution = restitution;
			this.drag = drag;
			mass = this.collider.GetMass();
			iMass = 1 / mass;
			
		}

		public PhysicsObject(string fileName = null) : base(fileName)
		{
			collider = null;
			drag = 0;

		}



		public void Iterate(float deltaTime)
		{
			if (hasPhysics)
			{
				acceleration.y -= gravity * iMass;

				velocity.x += acceleration.y * deltaTime;
				velocity.x += acceleration.y * deltaTime;

				position.x += (velocity.x - velocity.x * drag) * deltaTime;
				position.y += (velocity.y - velocity.y * drag) * deltaTime;

			}
			foreach (var child in children)
			{
				if (child is PhysicsObject)
					((PhysicsObject)child).Iterate(deltaTime);

			}
			acceleration.x = 0;
			acceleration.y = 0;
		}
	}
}
