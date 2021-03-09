using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
	public struct Vector2
	{
		public float x, y;

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		#region Properties
		

			#region Default Vectors
				public static Vector2 Zero
				{
					get
					{
						return new Vector2(0, 0);
					}
				}

				public static Vector2 One
				{
					get
					{
						return new Vector2(1, 1);
					}
				}

				public static Vector2 Up
				{
					get
					{
						return new Vector2(0, 1);
					}
				}

				public static Vector2 Down
				{
					get
					{
						return new Vector2(0, -1);
					}
				}

				public static Vector2 Left
				{
					get
					{
						return new Vector2(-1, 0);
					}
				}

				public static Vector2 Right
				{
					get
					{
						return new Vector2(1, 0);
					}
				}
			#endregion
		#endregion

		#region Methods
			public float Dot(Vector2 b)
			{
				return x * b.x + y * b.y;
			}

			public static float Dot(Vector2 a, Vector2 b)
			{
				return a.x * b.x + a.y * b.y;
			}

			public void Rotate(float angle)
			{
				float s = (float)Math.Sin(angle);
				float c = (float)Math.Cos(angle);
				float prevX = x;
				x = x * c - y * s;
				y = prevX * s + y * c;
			}

			public static Vector2 Rotate(Vector2 v, float angle)
			{
				float s = (float)Math.Sin(angle);
				float c = (float)Math.Cos(angle);
				float prevX = v.x;
				v.x = v.x * c - v.y * s;
				v.y = prevX * s + v.y * c;
				return v;
			}

			public float Magnitude()
			{
				return (float)Math.Sqrt(x * x + y * y);
			}

			public float MagnitudeSquared()
			{
				return x * x + y * y;
			}

			public void Normalize()
			{
				float iLength = 1 / Magnitude();
				x = x * iLength;
				y = y * iLength;
			}
		#endregion

		#region Operator Overloaders

			public static Vector2 operator +(Vector2 a, Vector2 b)
			{
				return new Vector2(a.x + b.x, a.y + b.y);
			}

			public static Vector2 operator -(Vector2 a, Vector2 b)
			{
				return new Vector2(a.x - b.x, a.y - b.y);
			}

			public static Vector2 operator *(Vector2 v, float f)
			{
				return new Vector2(v.x * f, v.y * f);
			}

			public static Vector2 operator *(float f, Vector2 v)
			{
				return new Vector2(v.x * f, v.y * f);
			}

			public static Vector2 operator *(Vector2 a, Vector2 b)
			{
				return new Vector2(a.x * b.x, a.y * b.y);
			}

			public static Vector2 operator /(Vector2 v, float f)
			{
				return new Vector2(v.x / f, v.y / f);
			}

			public static Vector2 operator /(Vector2 a, Vector2 b)
			{
				return new Vector2(a.x / b.x, a.y / b.y);
			}

			#region Useless Overloaders

				public static Vector2 operator ++(Vector2 v)
				{
					return new Vector2(v.x + 1, v.y + 1);
				}

				public static Vector2 operator --(Vector2 v)
				{
					return new Vector2(v.x - 1, v.y - 1);
				}

				public static bool operator true(Vector2 v)
				{
					return (v.x != 0 && v.y != 0);
				}

				public static bool operator false(Vector2 v)
				{
					return (v.x == 0 && v.y == 0);
				}

				public static bool operator >(Vector2 a, Vector2 b)
				{
					return a.MagnitudeSquared() > b.MagnitudeSquared();
				}

				public static bool operator <(Vector2 a, Vector2 b)
				{
					return a.MagnitudeSquared() < b.MagnitudeSquared();
				}

				public static bool operator >(Vector2 v, float f)
				{
					return v.MagnitudeSquared() > f*f;
				}

				public static bool operator <(Vector2 v, float f)
				{
					return v.MagnitudeSquared() < f*f;
				}

			#endregion
		#endregion

	}
}
