using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mlib;


namespace Project2D
{
	class PhysicsObject : GameObject
	{
		Collider collider;
		
		Vector2 velocity;
		float angularVelocity;

		float restitution;
		float mass;
		

		
		public void Iterate()
		{
			if (hasPhysics)
			{

			}
			foreach (var child in children)
			{
				if (child is PhysicsObject)
					((PhysicsObject)child).Iterate();

			}
		}
	}
}
