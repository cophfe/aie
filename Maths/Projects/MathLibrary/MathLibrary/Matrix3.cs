using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mlib
{
	public class Matrix3
	{

		public float[] m;

		//float m11 = 1; float m12 = 0; float m13 = 0;
		//float m21 = 0; float m22 = 1; float m23 = 0;
		//float m31 = 0; float m32 = 0; float m33 = 1;

		public Matrix3(float m1 = 1, float m4 = 0, float m7 = 0,
						float m2 = 0, float m5 = 1, float m8 = 0,
						float m3 = 0, float m6 = 0, float m9 = 1)
		{

			m = new float[] { m1, m3, m6, 
								m2, m4, m7,
								m3, m5, m8 };
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

		public static Vector2 operator *(Matrix3 m, Vector2 v)
		{
			return new Vector2(m.m[0] * v.x + m.m[3] * v.y,
								m.m[1] * v.x + m.m[5] * v.y);
		}

		public static Matrix3 operator *(Matrix3 a, Matrix3 b)
		{
			Matrix3 m = new Matrix3(1);
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					m.m[x + 3 * y] = a.m[x] * b.m[3 * y] + a.m[x + 3] * b.m[4 * y] + a.m[x + 6] * b.m[5 * y];
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

			m[0] = 1; m[3] = 0; m[6] = 0;
			m[1] = 0; m[4] = cos; m[7] = sin;
			m[2] = 0; m[5] = -sin; m[8] = cos;
		}

		public void SetRotateY(float angle)
		{
			if (m == null)
				m = new float[9];

			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = cos; m[3] = 0; m[6] = -sin;
			m[1] = 0; m[4] = 1; m[7] = 0;
			m[2] = sin; m[5] = 0; m[8] = cos;
		}

		public void SetRotateZ(float angle)
		{
			if (m == null)
				m = new float[9];

			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = cos; m[3] = -sin; m[6] = 0;
			m[1] = sin; m[4] = cos; m[7] = 0;
			m[2] = 0; m[5] = 0; m[8] = 1;
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

		// 0 1 2
		// 3 4 5 
		// 6 7 8

		public void SetTranslation(float x, float y)
		{
			m[2] = x;
			m[5] = y;
			m[8] = 1;
		}

		public void SetTranslation(Vector2 pos)
		{
			m[2] = pos.x;
			m[5] = pos.y;
			m[8] = 1;
		}

		public void AddTranslation(float x, float y)
		{
			m[2] += x;
			m[5] += y;
		}

		public void AddTranslation(Vector2 pos)
		{
			m[2] += pos.x;
			m[5] += pos.y;
		}

		public void SetScale(float x, float y)
		{
			m[0] = x; m[1] = 0; m[2] = 0; 
			m[3] = 0; m[4] = y; m[5] = 0; 
			m[6] = 0; m[7] = 0; m[8] = 1;
		}

		public void SetScale(Vector2 scale)
		{
			m[0] = scale.x; m[1] = 0;		m[2] = 0;
			m[3] = 0;		m[4] = scale.y; m[5] = 0;
			m[6] = 0;		m[7] = 0;		m[8] = 1;
		}

		public static Matrix3 GetScale(float x, float y)
		{
			return new Matrix3(x, m4: y);
		}

		public static Matrix3 GetScale(Vector2 scale)
		{
			return new Matrix3(scale.x, m4: scale.y);
		}

		public static Matrix3 GetRotateX(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix3(1, 0, 0, 0, cos, sin, 0, -sin, cos);
		}

		public static Matrix3 GetRotateY(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix3(cos, 0, -sin, 0, 1, 0, sin, 0, cos);
		}

		public static Matrix3 GetRotateZ(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix3(cos, sin, 0, -sin, cos, 0, 0, 0, 1);
		}

		public static Matrix3 GetTranslation(Vector2 pos)
		{
			return new Matrix3(m6: pos.x, m7: pos.y);
		}

		public static Matrix3 GetTranslation(float x, float y)
		{
			return new Matrix3(m6: x, m7: y);
		}

		public Vector2 GetTranslation()
		{
			return new Vector2(m[2], m[5]);
		}
	}
}
