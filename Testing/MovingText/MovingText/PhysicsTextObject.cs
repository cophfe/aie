using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovingText
{
	class PhysicsTextObject : TextObject
	{
		public PhysicsTextObject(Vector2 position, Vector2 scale, Vector2 velocity, float rotation, float angularVelocity, float inertia, float zValue, float gravity) : base(position, scale, rotation, zValue)
		{
			
		}

		bool simulated;
		float angularDrag;
		float angularVelocity;
		float drag;
		Vector2 velocity;
		public float inertia;
		float gravityScale;

		public void Iterate()
		{
			velocity.Y -= gravityScale;
			rotation += angularVelocity;
			position += velocity;

		}
	}

	class CollisionBox
	{
		Vector2 rotationalAxis;
		
		public CollisionBox()
		{

		}
	}
}
