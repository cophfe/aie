using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mlib
{
	public struct Matrix3
	{

		public float[] m;


		public Matrix3(float m00 = 1, float m01 = 0, float m02 = 0,
						float m10 = 0, float m11 = 1, float m12 = 0,
						float m20 = 0, float m21 = 0, float m22 = 1)
		{

			m = new float[] { m00, m01, m02, m10, m11, m12, m20, m21, m22 };
		}



		public static Matrix3 Identity
		{
			get { return new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1); }
		}

		public static Matrix3 Zero
		{
			get { return new Matrix3(0, 0, 0, 0, 0, 0, 0, 0, 0); }
		}

		public static Vector3 operator *(Matrix3 m, Vector3 v)
		{
			return new Vector3(m.m[0] * v.x + m.m[3] * v.y + m.m[6] * v.z,
								m.m[1] * v.x + m.m[4] * v.y + m.m[7] * v.z,
								m.m[2] * v.x + m.m[5] * v.y + m.m[8] * v.z);
		}

		public static Matrix3 operator *(Matrix3 a, Matrix3 b)
		{
			Matrix3 m = new Matrix3(1);
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					m.m[x + 3 * y] = a.m[x] * b.m[3 * y] + a.m[x + 3] * b.m[1 + 3 * y] + a.m[x + 6] * b.m[2 + 3 * y];
				}
			}
			return m;
		}

		public static Matrix3 operator +(Matrix3 m, float f)
		{
			return new Matrix3(m.m[0] + f, m.m[1] + f, m.m[2] + f,
								m.m[3] + f, m.m[4] + f, m.m[5] + f,
								m.m[6] + f, m.m[7] + f, m.m[8] + f);
		}

		public static Matrix3 operator +(float f, Matrix3 m)
		{
			return new Matrix3(m.m[0] + f, m.m[1] + f, m.m[2] + f,
								m.m[3] + f, m.m[4] + f, m.m[5] + f,
								m.m[6] + f, m.m[7] + f, m.m[8] + f);
		}

		public static Matrix3 operator +(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m[0] + b.m[0], a.m[1] + b.m[1], a.m[2] + b.m[2],
								a.m[3] + b.m[3], a.m[4] + b.m[4], a.m[5] + b.m[5],
								a.m[6] + b.m[6], a.m[7] + b.m[7], a.m[8] + b.m[8]);
		}
		public static Matrix3 operator -(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m[0] - b.m[0], a.m[1] - b.m[1], a.m[2] - b.m[2],
								a.m[3] - b.m[3], a.m[4] - b.m[4], a.m[5] - b.m[5],
								a.m[6] - b.m[6], a.m[7] - b.m[7], a.m[8] - b.m[8]);
		}

		public void SetRotateX(float angle)
		{
			if (m == null)
				m = new float[9];


			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = 1; m[1] = 0; m[2] = 0;
			m[3] = 0; m[4] = cos; m[5] = sin;
			m[6] = 0; m[7] = -sin; m[8] = cos;
		}

		public void SetRotateY(float angle)
		{
			if (m == null)
				m = new float[9];

			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = cos; m[1] = 0; m[2] = -sin;
			m[3] = 0; m[4] = 1; m[5] = 0;
			m[6] = sin; m[7] = 0; m[8] = cos;
		}

		public void SetRotateZ(float angle)
		{
			if (m == null)
				m = new float[9];

			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = cos; m[1] = sin; m[2] = 0;
			m[3] = -sin; m[4] = cos; m[5] = 0;
			m[6] = 0; m[7] = 0; m[8] = 1;
		}

		public Matrix3 Inverse()
		{
			//	0 3 6
			//	1 4 7
			//	2 5 8

			float d0 = m[4] * m[8] - m[5] * m[7], d3 = m[1] * m[8] - m[7] * m[2], d6 = m[1] * m[5] - m[4] * m[2];
			float det = m[0] * d0 - m[3] * d3 + m[6] * d6;
			Matrix3 i = Identity;
			if (det == 0)
				return i; // THERE IS NO INVERSE

			//transposes, cofactors minor-ises and divides by the determinant all in one step (god, please dont make me debug this)

			
			float iDet = 1/ det;
			i.m[0] = d0 * iDet;
			i.m[1] = -d3 * iDet;
			i.m[2] = d6 * iDet;
			i.m[3] = -(m[3] * m[8] - m[6] * m[5]) * iDet;
			i.m[4] = (m[0] * m[8] - m[6] * m[2]) * iDet;
			i.m[5] = -(m[0] * m[5] - m[3] * m[2]) * iDet;
			i.m[6] = (m[3] * m[7] - m[6] * m[4]) * iDet;
			i.m[7] = -(m[0] * m[7] - m[6] * m[1]) * iDet;
			i.m[8] = (m[0] * m[4] - m[3] * m[1]) * iDet;

			return i;

		}

	}
}
