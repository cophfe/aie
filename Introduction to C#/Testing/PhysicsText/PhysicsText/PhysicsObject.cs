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
		

		private Vector2 dragForce;
		private Vector2 force;
		public void Iterate(float deltaTime)
		{
			dragForce = Vector2.Normalize(-velocity) * velocity.LengthSquared() * drag;
			force = new Vector2(dragForce.X, dragForce.Y + mass *-gravityScale);
			velocity += force * deltaTime/mass;

			rotation += angularVelocity * deltaTime;
			position += velocity * deltaTime;

		}

		public static PhysicsObject Create(float startRotation, Vector2 startPosition, Vector2 scale, SizeF collisionBoxSize, float angularDrag, float angularVelocity, float drag, float gravityScale, float inertia)
		{
			PhysicsObject pO = new PhysicsObject();
			pO.isSimulated = true;
			pO.angularDrag = angularDrag;
			pO.angularVelocity = angularVelocity;
			pO.drag = drag;
			pO.gravityScale = gravityScale;
			pO.inertia = inertia;
			pO.position = startPosition;
			pO.rotation = startRotation;
			pO.scale = scale;
			pO.size = collisionBoxSize;
			pO.velocity = new Vector2(0,0);
			return pO;
		}
		
	}
}
