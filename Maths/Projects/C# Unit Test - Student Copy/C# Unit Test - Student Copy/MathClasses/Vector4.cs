using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public struct Vector4
	{
		public float x, y, z, w;

		public Vector4(float x = 0, float y = 0, float z = 0, float w = 1)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		#region Properties
			public static Vector4 Zero
			{
				get
				{
					return new Vector4(0, 0, 0);
				}
			}

			public static Vector4 One
			{
				get
				{
					return new Vector4(1, 1, 1);
				}
			}

		
		#endregion

		#region Methods
			public float Dot(Vector4 b)
			{
				return x * b.x + y * b.y + z * b.z + w * b.w;
			}

			public static float Dot(Vector4 a, Vector4 b)
			{
				return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
			}

			public float Magnitude()
			{
				return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
			}

			public float MagnitudeSquared()
			{
				return x * x + y * y + z * z + w * w;
			}

			public void Normalize()
			{
				float iLength = 1/Magnitude();
				x = x * iLength;
				y = y * iLength;
				z = z * iLength;
				w = w * iLength;
			}

			public Vector4 Cross(Vector4 b)
			{
				return new Vector4(y * b.z - b.y * z, z * b.x - b.z * x, x * b.y - b.x * y, 0);
			}
		#endregion

		#region Operator Overloaders

		public static Vector4 operator +(Vector4 a, Vector4 b)
			{
				return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
			}

			public static Vector4 operator -(Vector4 a, Vector4 b)
			{
				return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
			}

			public static Vector4 operator *(Vector4 v, float f)
			{
				return new Vector4(v.x * f, v.y * f, v.z * f, v.w * f);
			}

			public static Vector4 operator *(float f, Vector4 v)
			{
				return new Vector4(v.x * f, v.y * f, v.z * f, v.w * f);
			}

			public static Vector4 operator *(Vector4 a, Vector4 b)
			{
				return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
			}

			public static Vector4 operator /(Vector4 v, float f)
			{
				return new Vector4(v.x / f, v.y / f, v.z / f, v.w / f);
			}

			public static Vector4 operator /(Vector4 a, Vector4 b)
			{
				return new Vector4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
			}

			#region Useless Overloaders

				public static Vector4 operator ++(Vector4 v)
				{
					return new Vector4(v.x + 1, v.y + 1, v.z + 1, v.w + 1);
				}

				public static Vector4 operator --(Vector4 v)
				{
					return new Vector4(v.x - 1, v.y - 1, v.z - 1, v.w + 1);
				}

				public static bool operator true(Vector4 v)
				{
					return (v.x != 0 && v.y != 0 && v.z != 0 && v.w != 0);
				}

				public static bool operator false(Vector4 v)
				{
					return (v.x == 0 && v.y == 0 && v.z == 0 && v.w == 0);
				}

				public static bool operator >(Vector4 a, Vector4 b)
				{
					return a.MagnitudeSquared() > b.MagnitudeSquared();
				}

				public static bool operator <(Vector4 a, Vector4 b)
				{
					return a.MagnitudeSquared() < b.MagnitudeSquared();
				}

				public static bool operator >(Vector4 v, float f)
				{
					return v.MagnitudeSquared() > f * f;
				}

				public static bool operator <(Vector4 v, float f)
				{
					return v.MagnitudeSquared() < f * f;
				}
			#endregion
		#endregion

	}
}
