using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsText
{
	class PhysicsObject
	{



		/// <summary>
		/// Controls if physics affects the object.
		/// </summary>
		public bool isSimulated;
		/// <summary>
		/// The angular drag of the object.
		/// </summary>
		public float angularDrag;
		/// <summary>
		/// The angular velocity of the object in degrees per second (counterclockwise).
		/// </summary>
		public float angularVelocity;
		/// <summary>
		/// The drag of the object.
		/// </summary>
		public float drag;
		/// <summary>
		/// The 2D velocity of the object (units per second)
		/// </summary>
		public Vector2 velocity;
		/// <summary>
		/// The rotational inertia (units per second).
		/// </summary>
		public float inertia;
		/// <summary>
		/// The gravitational acceleration applied to the object.
		/// </summary>
		public float gravityScale;
		/// <summary>
		/// the 2D scaling multiplier for the object.
		/// </summary>
		public Vector2 scale;
		/// <summary>
		/// The 2D position of the object on screen.
		/// </summary>
		public Vector2 position;
		/// <summary>
		/// The current rotation of the object counterclockwise.
		/// </summary>
		public float rotation;
		/// <summary>
		/// The 2D size of the object's collision box
		/// </summary>
		public SizeF size;
		/// <summary>
		/// the mass of the object
		/// </summary>
		public float mass = 1;
		

		private Vector2 dragForce = new Vector2(0,0);
		private Vector2 force = new Vector2(0,0);
		public void Iterate(float deltaTime)
		{
			//dragForce = velocity * velocity.Length() * drag;
			force.X = -dragForce.X;
			force.Y = -(-dragForce.Y + mass *-gravityScale);
			velocity += force * deltaTime/mass;
			rotation %= 360;
			rotation += angularVelocity * deltaTime;
			position.X += velocity.X * deltaTime;
			position.Y += velocity.Y * deltaTime;

		}

		
	}
}
