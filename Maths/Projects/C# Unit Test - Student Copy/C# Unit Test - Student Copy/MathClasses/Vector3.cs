using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
	public struct Vector3
	{
		public float x, y, z;

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			z = 0;
		}

		#region Properties
		#region Default Vectors
		public static Vector3 Zero
		{
			get
			{
				return new Vector3(0, 0, 0);
			}
		}

		public static Vector3 One
		{
			get
			{
				return new Vector3(1, 1, 1);
			}
		}

		public static Vector3 Up
		{
			get
			{
				return new Vector3(0, 1, 0);
			}
		}

		public static Vector3 Down
		{
			get
			{
				return new Vector3(0, -1, 0);
			}
		}

		public static Vector3 Left
		{
			get
			{
				return new Vector3(-1, 0, 0);
			}
		}

		public static Vector3 Right
		{
			get
			{
				return new Vector3(1, 0, 0);
			}
		}

		public static Vector3 Forward
		{
			get
			{
				return new Vector3(0, 0, 1);
			}
		}

		public static Vector3 Backward
		{
			get
			{
				return new Vector3(0, 0, -1);
			}
		}
		#endregion
		#endregion

		#region Methods
		public float Dot(Vector3 b)
		{
			return x * b.x + y * b.y + z * b.z;
		}

		public Vector3 Cross(Vector3 b)
		{
			return new Vector3(y * b.z - b.y * z, z * b.x - b.z * x, x * b.y - b.x * y);
		}

		public static Vector3 Cross(Vector3 a, Vector3 b)
		{
			return new Vector3(a.y * b.z - b.y * a.z, a.z * b.x - b.z * a.x, a.x * b.y - b.x * a.y);
		}

		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public float Magnitude()
		{
			return (float)Math.Sqrt(x * x + y * y + z * z);
		}

		public float MagnitudeSquared()
		{
			return x * x + y * y + z * z;
		}

		public void Normalize()
		{
			float iLength = 1 / Magnitude();
			x = x * iLength;
			y = y * iLength;
			z = z * iLength;
		}
		#endregion

		#region Operator Overloaders

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator *(Vector3 v, float f)
		{
			return new Vector3(v.x * f, v.y * f, v.z * f);
		}

		public static Vector3 operator *(float f, Vector3 v)
		{
			return new Vector3(v.x * f, v.y * f, v.z * f);
		}

		public static Vector3 operator *(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static Vector3 operator /(Vector3 v, float f)
		{
			return new Vector3(v.x / f, v.y / f, v.z / f);
		}

		public static Vector3 operator /(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		#region Useless Overloaders

		public static Vector3 operator ++(Vector3 v)
		{
			return new Vector3(v.x + 1, v.y + 1, v.z + 1);
		}

		public static Vector3 operator --(Vector3 v)
		{
			return new Vector3(v.x - 1, v.y - 1, v.z - 1);
		}

		public static bool operator true(Vector3 v)
		{
			return (v.x != 0 && v.y != 0 && v.z != 0);
		}

		public static bool operator false(Vector3 v)
		{
			return (v.x == 0 && v.y == 0 && v.z == 0);
		}

		public static bool operator >(Vector3 a, Vector3 b)
		{
			return a.MagnitudeSquared() > b.MagnitudeSquared();
		}

		public static bool operator <(Vector3 a, Vector3 b)
		{
			return a.MagnitudeSquared() < b.MagnitudeSquared();
		}

		public static bool operator >(Vector3 v, float f)
		{
			return v.MagnitudeSquared() > f * f;
		}

		public static bool operator <(Vector3 v, float f)
		{
			return v.MagnitudeSquared() < f * f;
		}
		#endregion
		#endregion

	}
}
