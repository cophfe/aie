using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MovingText
{
	class TextObject
	{

		public TextObject(Vector2 position, Vector2 scale, float rotation, float zValue)
		{
			enabled = true;
			this.position = position;
			this.scale = scale;
			this.rotation = rotation;
			z = zValue;
		}

		public bool enabled = true;
		public Vector2 scale;
		public Vector2 position;
		public float z;
		public float rotation;

		TextObject parent = null;
		

	}
}
