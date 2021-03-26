using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsText
{
	class CharRenderObject : PhysicsObject
	{
		/// <summary>
		/// The character this object looks like
		/// </summary>
		public Char character;
		/// <summary>
		/// Controls if the object is rendered or not.
		/// </summary>
		public bool isEnabled = true;
		/// <summary>
		/// The z axis value (used to determine the order of rendering).
		/// </summary>
		float z;

		public static void GenerateObjectFromChar(char character, Font font, Graphics g, Size size)
		{
			
		}

		public static void GenerateObjectFromMonoChar(char character, Font font, Graphics g)
		{

		}

		public static SizeF FindMonospaceCharSizeFromFont(Font font, Graphics graphics)
		{
			return graphics.MeasureString("X", font);
		}

		public static CharRenderObject CreateCharObject(char character, float startRotation, Vector2 startPosition, Vector2 scale, SizeF collisionBoxSize, float angularDrag, float angularVelocity, Vector2 velocity, float drag, float gravityScale, float inertia)
		{
			CharRenderObject cRO = new CharRenderObject();
			cRO.isSimulated = true;
			cRO.angularDrag = angularDrag;
			cRO.angularVelocity = angularVelocity;
			cRO.drag = drag;
			cRO.gravityScale = gravityScale;
			cRO.inertia = inertia;
			cRO.position = startPosition;
			cRO.velocity = velocity;
			cRO.rotation = startRotation;
			cRO.scale = scale;
			cRO.size = collisionBoxSize;
			cRO.character = character;
			return cRO;
		}
	}
}
