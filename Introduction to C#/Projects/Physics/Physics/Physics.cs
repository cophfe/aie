using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Physics
{
	class Physics
	{


		#region Maths
		
		/// <summary>
		/// The (psuedo) cross product of two 2D vectors.
		/// </summary>
		/// <returns> The value of the 3D cross product on the z axis (only non zero value when using 2D vectors) </returns>
		public static float CrossProduct(Vector2 a, Vector2 b)
		{
			return a.X * b.Y - b.X * a.Y;
		}

		/// <summary>
		/// The (pseudo) cross product of a 2D vector and a scalar
		/// </summary>
		/// <returns>The input vector rotated by 90 degrees counterclockwise and also multiplied on each axis by the input scalar</returns>
		public static Vector2 CrossProduct(Vector2 v, float s)
		{
			return new Vector2(s * v.Y, -s * v.X);
		}

		/// <summary>
		/// The (pseudo) cross product of a 2D vector and a scalar
		/// </summary>
		/// <returns>The input vector rotated by 90 degrees clockwise and also multiplied on each axis by the input scalar</returns>
		public static Vector2 CrossProduct(float s, Vector2 v)
		{
			return new Vector2(-s * v.Y, s * v.X);
		}
		#endregion

		#region Collision

		const float percent = 0.6f;

		public static bool CircleCircle(ref CollisionPair pair)
		{
			Vector2 normal = pair.b.position - pair.a.position;
			float radius = pair.b.radius + pair.a.radius;
			if (normal.LengthSquared() > radius * radius) //this calculates if circles are colliding
			{
				return false;
			}



			float distance = normal.Length(); //distance between circle centers

			if (distance != 0)
			{
				pair.penetration = radius - distance;

				pair.normal = normal / distance;

				return true;
			}
			else
			{
				pair.penetration = pair.a.radius;
				pair.normal = new Vector2(1, 0);
				return true;
			}
		}

		/// <summary>
		/// Does a quick check to see if the AABB of a circle has collided with the scene bounds (an inverted rectange). 
		/// This check is equivelant to checking if a circle has collided with the scene bounds.
		/// </summary>
		public static bool CheckCircleBounds(CircleObject circle, Bounds bounds)
		{
			return circle.position.X - circle.radius < bounds.topLeft.X || circle.position.X + circle.radius > bounds.bottomRight.X ||
				circle.position.Y - circle.radius < bounds.topLeft.Y || circle.position.Y + circle.radius > bounds.bottomRight.Y;
		}
		public static void ResolveBoundsCollision(CircleObject circle, Bounds bounds)
		{
			//there are 4 different situations here since the bounds object is basically 4 rectangles
			//2 situations can happen at the same time

			Vector2 normal = new Vector2();
			float correction = 0;

			if (circle.position.X - circle.radius < bounds.topLeft.X)
			{
				normal.X = 1;
				correction = circle.position.X - circle.radius + bounds.topLeft.X;
			}
			else if (circle.position.X + circle.radius > bounds.bottomRight.X)
			{
				normal.X = -1;
				correction = circle.position.X + circle.radius - bounds.bottomRight.X;
			}

			if (circle.position.Y - circle.radius < bounds.topLeft.Y)
			{
				normal.Y = 1;
				correction = circle.position.Y - circle.radius + bounds.topLeft.Y;
			}
			else if (circle.position.Y + circle.radius > bounds.bottomRight.Y)
			{
				normal.Y = -1;
				correction = circle.position.Y + circle.radius - bounds.bottomRight.Y;
			}

			Vector2 impulse;
			float dot = Vector2.Dot(normal, circle.velocity);
			if (dot > 0)
				return;
			float mag = -(1 + circle.bounce) * dot / (circle.iMass + (circle.radius * circle.radius * normal.LengthSquared() * circle.iInertia));
			impulse = mag * normal;
			circle.velocity += impulse * circle.iMass;
			//circle.angularVelocity += circle.iInertia * CrossProduct(new Vector2(circle.radius, circle.radius), impulse);

			//friction
			//FRICTION
			dot = Vector2.Dot(circle.velocity, normal);
			Vector2 tangent = Vector2.Normalize(circle.velocity - dot * normal);
			float fMag = -Vector2.Dot(circle.velocity, tangent) / (circle.iMass + (circle.radius * circle.radius * normal.LengthSquared() * circle.iInertia));

			if (Math.Abs(fMag) < mag * circle.staticFriction)
				impulse = fMag * tangent;
			else
			{
				impulse = -mag * tangent * circle.dynamicFriction;
			}
			circle.velocity += impulse * circle.iMass;
			//circle.angularVelocity += circle.iInertia * CrossProduct(new Vector2(circle.radius, circle.radius), impulse);

			circle.position += correction * percent * normal;
		}

		public static void ResolveCollision(CollisionPair pair)
		{
			//velocity of A relative to B
			Vector2 rV = pair.b.velocity - pair.a.velocity;

			// dot product between the collision normal and the relative velocity
			float dot = Vector2.Dot( rV, pair.normal);

			if (dot > 0) //means they are moving away from eachother
				return;

			//restitution coefficient used (there is only ever one per collision but our simulation requires one for each object)
			float rest = Math.Min(pair.a.bounce, pair.b.bounce);
			float nMagSqr = pair.normal.LengthSquared();
			float mag = (-(1 + rest) * dot) / (pair.a.iMass + pair.b.iMass + (pair.a.radius * pair.a.radius * nMagSqr * pair.a.iInertia) + (pair.b.radius * pair.b.radius * nMagSqr * pair.b.iInertia));
			Vector2 impulse = pair.normal * mag; 

			//delta v = delta p / mass
			pair.b.velocity += impulse * pair.b.iMass;
			pair.a.velocity -= impulse * pair.a.iMass;
			pair.a.angularVelocity -= pair.a.iInertia * CrossProduct(pair.normal, impulse);
			pair.b.angularVelocity += pair.b.iInertia * CrossProduct(pair.normal, impulse);

			//FRICTION
			rV = pair.b.velocity - pair.a.velocity;
			dot = Vector2.Dot(rV, pair.normal);
			Vector2 tangent = Vector2.Normalize(rV - dot * pair.normal);
			float tMagSqr = tangent.LengthSquared();
			//float fMag = - Vector2.Dot(rV,tangent) / (pair.a.iMass + pair.b.iMass);
			float fMag = - Vector2.Dot(rV,tangent) / (pair.a.iMass + pair.b.iMass + (pair.a.radius * pair.a.radius * tMagSqr * pair.a.iInertia) + (pair.b.radius * pair.b.radius * tMagSqr * pair.b.iInertia));

			//friction coefficient estimation
			float co = (pair.a.staticFriction + pair.b.staticFriction)/2;

			if (Math.Abs(fMag) < mag * co)
				impulse = fMag * tangent;
			else
			{
				co = (pair.a.dynamicFriction + pair.b.dynamicFriction) / 2;
				impulse = -mag * tangent * co;
			}
			pair.a.velocity -= impulse * pair.a.iMass;
			pair.b.velocity += impulse * pair.b.iMass;

			pair.a.angularVelocity -= pair.a.iInertia * CrossProduct(new Vector2(pair.a.radius, pair.a.radius), impulse);
			pair.b.angularVelocity += pair.b.iInertia * CrossProduct(new Vector2(pair.a.radius, pair.a.radius), impulse);
			



		}

		
		public static void CorrectPosition(CollisionPair pair)
		{
			Vector2 correction = pair.penetration / (pair.a.iMass + pair.b.iMass) * percent * pair.normal;
			pair.a.position -= pair.a.iMass * correction;
			pair.b.position += pair.b.iMass * correction;
		}
		#endregion

		#region Physics
		public static List<CollisionPair> pairList = new List<CollisionPair>();
		public static List<CollisionPair> uniquePairList = new List<CollisionPair>();
		public static List<CircleObject> objectList = new List<CircleObject>();
		public static Bounds sceneBounds;

		public static void Iterate(double dt)
		{
			pairList.Clear();
			uniquePairList.Clear();
			CollisionPair abPair;
			
			for (int a = 0; a < objectList.Count; a++)
			{
				
				for (int b = 0; b < objectList.Count; b++)
				{
					if (a == b)
						continue;

					abPair = new CollisionPair(objectList[a], objectList[b]);
					if (CircleCircle(ref abPair)) 
					{
						pairList.Add(abPair);
					}
				}
			}

			pairList.Sort();
			for (int i = 0; i < pairList.Count - 1; i++)
			{
				uniquePairList.Add(pairList[i]);
				i++;
				while (i < pairList.Count - 1)
				{
					if (pairList[i].Tag != pairList[i + 1].Tag)
					{
						break;
					}
					i++;
				}
			}
			for (int i = 0; i < uniquePairList.Count; i++)
			{
				ResolveCollision(uniquePairList[i]);
				CorrectPosition(uniquePairList[i]);
			}

			for (int i = 0; i < objectList.Count; i++)
			{
				if (CheckCircleBounds(objectList[i], sceneBounds))
				{
					ResolveBoundsCollision(objectList[i], sceneBounds);
				}

				objectList[i].Iterate(dt);
			}
		}


		#endregion
	}

	struct Bounds
	{
		public Vector2 topLeft;
		public Vector2 bottomRight;

		public Bounds(Vector2 topLeft, Vector2 bottomRight)
		{
			this.topLeft = topLeft;
			this.bottomRight = bottomRight;
		}
	}
	class CollisionPair : IComparable
	{
		public CircleObject a;
		public CircleObject b;
		public float penetration;
		public Vector2 normal;

		public uint Tag
		{
			get
			{
				return a.Tag * b.Tag;
			}
		}
		public CollisionPair(CircleObject a, CircleObject b)
		{
			this.a = a;
			this.b = b;
			normal = Vector2.Zero;
			penetration = 0;
		}

		public CollisionPair(CircleObject a, CircleObject b, Vector2 normal, float penetration)
		{
			this.a = a;
			this.b = b;
			this.normal = normal;
			this.penetration = penetration;
		}

		public int CompareTo(object obj)
		{
			CollisionPair compare = (CollisionPair)obj;
			if (Tag == compare.Tag)
			{
				return 0;
			}
			return 1;
		}
	}


	class CircleObject
	{
		Random rand = new Random();
		//Position stuff
		public Vector2 position;
		public float rotation;

		public Vector2 lastPosition;
		public float lastRotation;

		//Movement stuff
		public Vector2 force;
		public float torque;
		public Vector2 velocity;
		public float angularVelocity;
		public float gravityScale;
		public float drag, angularDrag;

		//Mass stuff
		public float mass;
		public float iMass;
		public float inertia;
		public float iInertia;
		public float density;
		uint tag;

		public uint Tag
		{
			get
			{
				return tag;
			}
		}

		//friction stuff
		public float staticFriction;
		public float dynamicFriction;
		public float bounce; //the coefficient of restitution (1 is perfectly elastic, 0 is perfectly inelastic)
		//IRL it can be more than one if a gain in kinetic energy takes place, such as due to a chemical reaction or due to a loss of rotational energy
		//I will just keep it constant

		//Collision and Rendering stuff
		public float radius;

		bool dynamic;

		public CircleObject(Vector2 position, float rotation, float density, float friction, float restitution, float radius, float gravity, float drag, float angularDrag, bool isDynamic)
		{
			this.position = position;
			this.rotation = rotation;
			dynamicFriction = friction;
			staticFriction = friction; // should change this at some point
			bounce = restitution;
			gravityScale = gravity;
			this.radius = radius;
			mass = CalculateMass(density);
			iMass = 1 / mass;
			inertia = MathF.PI * radius * radius * radius * radius / 4;
			iInertia = 1 / inertia;
			dynamic = isDynamic;
			tag = (uint)rand.Next(); //has a small chance to give the same tag to two different objects
			this.drag = drag;
			this.angularDrag = angularDrag;
		}

		float CalculateMass(float density)
		{
			return MathF.PI * radius * radius;
		}

		
		public void Iterate(double deltaTime)
		{
			if (dynamic)
			{
				force = Vector2.Zero;
				force.Y += (float)(deltaTime * gravityScale);
				velocity.X += (float)((iMass * force.X) * deltaTime);
				velocity.Y += (float)((iMass * force.Y) * deltaTime);
				if (velocity.X > 0)
				{
					velocity.X -= (float)(drag * deltaTime);
				}else velocity.X += (float)(drag * deltaTime);
				if (velocity.Y > 0)
				{
					velocity.Y -= (float)(drag * deltaTime);
				} else velocity.Y += (float)(drag * deltaTime);
				if (float.IsNaN(velocity.X))
					velocity.X = 0;
				if (float.IsNaN(velocity.Y))
					velocity.Y = 0;
				position.X += (float)(velocity.X * deltaTime);
				position.Y += (float)(velocity.Y * deltaTime);

				angularVelocity += (float)(torque * iInertia * deltaTime);
				if (angularVelocity > 0)
				{ 
					angularVelocity -= (float)(angularDrag * deltaTime);
				} else angularVelocity += (float)(angularDrag * deltaTime);
				rotation += (float)(angularVelocity * deltaTime );
				rotation %= MathF.PI * 2;

			}
			
		}

	}

}
