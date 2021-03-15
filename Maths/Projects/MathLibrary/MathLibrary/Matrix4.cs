using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mlib
{
	public struct Matrix4
	{
		public float[] m;

		public Matrix4(float m00 = 1, float m01 = 0, float m02 = 0, float m03 = 0,
						float m10 = 0, float m11 = 1, float m12 = 0, float m13 = 0,
						float m20 = 0, float m21 = 0, float m22 = 1, float m23 = 0,
						float m30 = 0, float m31 = 0, float m32 = 0, float m33 = 1)
		{

			m = new float[] { m00, m01, m02, m03,
				m10, m11, m12, m13,
				m20, m21, m22, m23, 
				m30, m31, m32, m33 };
		}



		public static Matrix4 Identity
		{
			get {
				return new Matrix4(1, 0, 0, 0,
								  0, 1, 0, 0,
								  0, 0, 1, 0,
								  0, 0, 0, 1); 
			}
		}

		public static Matrix4 Zero
		{
			get { return new Matrix4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); }
		}

		public static Vector4 operator *(Matrix4 m, Vector4 v)
		{
			return new Vector4(m.m[0] * v.x + m.m[4] * v.y + m.m[8] * v.z + m.m[12] * v.w,
								m.m[1] * v.x + m.m[5] * v.y + m.m[9] * v.z + m.m[13] * v.w,
								m.m[2] * v.x + m.m[6] * v.y + m.m[10] * v.z + m.m[14] * v.w,
								m.m[3] * v.x + m.m[7] * v.y + m.m[11] * v.z + m.m[15] * v.w);
		}

		public static Matrix4 operator *(Matrix4 a, Matrix4 b)
		{
			Matrix4 m = new Matrix4(1);
			for (int x = 0; x < 4; x++)
			{
				for (int y = 0; y < 4; y++)
				{
					m.m[x + 4 * y] = a.m[x] * b.m[4 * y] + a.m[x + 4] * b.m[1 + 4 * y] + a.m[x + 8] * b.m[2 + 4 * y] + a.m[x + 12] * b.m[3 + 4 * y];
				}
			}
			return m; 
		}

		public static Matrix4 operator +(Matrix4 m, float f)
		{
			return new Matrix4(m.m[0] + f, m.m[1] + f, m.m[2] + f, m.m[3] + f,
								m.m[4] + f, m.m[5] + f, m.m[6] + f, m.m[7] + f,
								m.m[8] + f, m.m[9] + f, m.m[10] + f, m.m[11] + f,
								m.m[12] + f, m.m[13] + f, m.m[14] + f, m.m[15] + f);
		}

		public static Matrix4 operator +(float f, Matrix4 m)
		{
			return new Matrix4(m.m[0] + f, m.m[1] + f, m.m[2] + f, m.m[3] + f,
								m.m[4] + f, m.m[5] + f, m.m[6] + f, m.m[7] + f,
								m.m[8] + f, m.m[9] + f, m.m[10] + f, m.m[11] + f,
								m.m[12] + f, m.m[13] + f, m.m[14] + f, m.m[15] + f);
		}

		public static Matrix4 operator +(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m[0] + b.m[0], a.m[1] + b.m[1], a.m[2] + b.m[2], a.m[3] + b.m[3],
								a.m[4] + b.m[4], a.m[5] + b.m[5], a.m[6] + b.m[6], a.m[7] + b.m[7],
								a.m[8] + b.m[8], a.m[9] + b.m[9], a.m[10] + b.m[10], a.m[11] + b.m[11],
								a.m[8] + b.m[8], a.m[9] + b.m[9], a.m[10] + b.m[10], a.m[15] + b.m[15]);
		}
		public static Matrix4 operator -(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m[0] - b.m[0], a.m[1] - b.m[1], a.m[2] - b.m[2], a.m[3] - b.m[3],
								a.m[4] - b.m[4], a.m[5] - b.m[5], a.m[6] - b.m[6], a.m[7] - b.m[7],
								a.m[8] - b.m[8], a.m[9] - b.m[9], a.m[10] - b.m[10], a.m[11] - b.m[11],
								a.m[8] - b.m[8], a.m[9] - b.m[9], a.m[10] - b.m[10], a.m[15] - b.m[15]);
		}

		public void SetRotateX(float angle)
		{
			if (m == null)
				m = new float[16];
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = 1;	m[1] = 0;		m[2] = 0;		//m[3] = 0;  
			m[4] = 0;	m[5] = cos;		m[6] = sin;		//m[7] = 0;  
			m[8] = 0;	m[9] = -sin;	m[10] = cos;	//m[11] = 0;  
			m[12] = 0;	m[13] = 0;		m[14] = 0;		//m[15] = 1;  
		}

		public void SetRotateY(float angle)
		{
			if (m == null) 
				m = new float[16];
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = cos; m[1] = 0; m[2] = -sin;	//m[3] = 0;
			m[4] = 0;	m[5] = 1; m[6] = 0;		//m[7] = 0;
			m[8] = sin; m[9] = 0; m[10] = cos;	//m[11] = 0;
			m[12] = 0; m[13] = 0; m[14] = 0;	//m[15] = 1;
		}

		public void SetRotateZ(float angle)
		{
			if (m == null)
				m = new float[16];
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m[0] = cos;		m[1] = sin; m[2] = 0;	//m[3] = 0;
			m[4] = -sin;	m[5] = cos; m[6] = 0;	//m[7] = 0;
			m[8] = 0;		m[9] = 0;	m[10] = 1;	//m[11] = 0;
			m[12] = 0;		m[13] = 0;	m[14] = 0;	//m[15] = 1;

		}


	}
}
