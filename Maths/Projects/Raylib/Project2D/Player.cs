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
	class Player : PhysicsObject
	{
		const float movementSpeedCap = 200f;
		const float rotateSpeedCap = 4f;
		const float movementAcceleration = 2000;
		const float rotateAcceleration = 10;


		public Player(string fileName, Vector2 position, Vector2 scale, float rotation, Scene scene) : base(fileName, position, scale, null, 0.5f, 3f, 0, 0, scene)
		{

		}

		Vector2 inputVelocity;
		public override void Update(float deltaTime)
		{

			if (IsKeyDown(KeyboardKey.KEY_W))
			{
				inputVelocity = (globalTransform.GetForwardVector() * -movementAcceleration * deltaTime);
				if ((velocity - inputVelocity).MagnitudeSquared() > movementSpeedCap * movementSpeedCap)
					inputVelocity = (inputVelocity + velocity).Normalised() * movementSpeedCap - velocity;
				velocity += inputVelocity;
				
				//velocity = velocity.Normalised() * movementSpeedCap;

				//clamp
				//if (velocity.MagnitudeSquared() > movementSpeedCap * movementSpeedCap)
				//{
				//	Vector2 d = velocity - velocity.Normalised() * movementSpeedCap;

				//}
				//else { 
				//}
			}
			if (IsKeyDown(KeyboardKey.KEY_S))
			{
				inputVelocity = (globalTransform.GetForwardVector() * movementAcceleration * deltaTime);
				if ((velocity - inputVelocity).MagnitudeSquared() > movementSpeedCap * movementSpeedCap)
					inputVelocity = (inputVelocity + velocity).Normalised() * movementSpeedCap - velocity;
				velocity += inputVelocity;
			}
			if (IsKeyDown(KeyboardKey.KEY_D))
			{
				//velocity -= globalTransform.GetRightVector() * movementSpeed;
				if (angularVelocity > -rotateSpeedCap)
					angularVelocity -= rotateAcceleration * deltaTime;
			}
			if (IsKeyDown(KeyboardKey.KEY_A))
			{
				if (angularVelocity < rotateSpeedCap)
					angularVelocity += rotateAcceleration * deltaTime;
			}
			

			base.Update(deltaTime);
		}
	}
}
