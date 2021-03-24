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
		static float velocityCap = 100f;
		static float rotateSpeedCap = 4f;
		static float accelerationCap = 600;
		static float rotateAcceleration = 10;
		static float frameAcceleration;

		const float movementFasterAddition = 200f;

		public Player(string fileName, Vector2 position, Vector2 scale, float rotation, Scene scene) : base(fileName, position, scale, null, 0.5f, 3f, 0, 0, scene)
		{

		}

		Vector2 inputVelocity;
		Vector2 inputDirection = Vector2.Zero;
		bool isMoving = false;

		public override void Update(float deltaTime)
		{
			isMoving = false;
			if (IsKeyDown(KeyboardKey.KEY_W))
			{
				inputDirection -= globalTransform.GetForwardVector();
				
				isMoving = true;
			}
			if (IsKeyDown(KeyboardKey.KEY_S))
			{
				inputDirection += globalTransform.GetForwardVector();
				isMoving = true;
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
			

			if (IsKeyPressed(KeyboardKey.KEY_LEFT_SHIFT))
			{
				velocity += globalTransform.GetForwardVector() * -1000;
			}

			
			inputVelocity = isMoving ? ((inputDirection * accelerationCap * deltaTime + velocity).Normalised() * velocityCap - velocity).Normalised() * accelerationCap * deltaTime : Vector2.Zero;
			velocity += inputVelocity;
			
			//if ((velocity - inputVelocity).MagnitudeSquared() > movementSpeedCap * movementSpeedCap)



			base.Update(deltaTime);
		}
	}
}
