using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mlib
{
	public class Matrix4
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

		public void SetTranslation(float x, float y, float z)
		{
			m[3] = x;
			m[7] = y;
			m[11] = z;
			m[15] = 1;
		}

		public void SetTranslation(Vector3 pos)
		{
			m[3] = pos.x;
			m[7] = pos.y;
			m[11] = pos.z;
			m[15] = 1;
		}

		public void SetScale(float x, float y, float z)
		{
			m[0] = x; m[1] = 0; m[2] = 0;   m[3] = 0;
			m[4] = 0; m[5] = y; m[6] = 0;  m[7] = 0;
			m[8] = 0; m[9] = 0; m[10] = z;  m[11] = 0;
			m[12] = 0; m[13] = 0; m[14] = 0;    m[15] = 1;
		}

		public void SetScale(Vector3 scale)
		{
			m[0] = scale.x; m[1] = 0; m[2] = 0; m[3] = 0;
			m[4] = 0; m[5] = scale.y; m[6] = 0; m[7] = 0;
			m[8] = 0; m[9] = 0; m[10] = scale.z; m[11] = 0;
			m[12] = 0; m[13] = 0; m[14] = 0; m[15] = 1;
		}

		public static Matrix3 GetScale(float x, float y, float z)
		{
			return new Matrix3(x, m11: y, m22: z);
		}

		public static Matrix3 GetScale(Vector3 scale)
		{
			return new Matrix3(scale.x, m11: scale.y, m22: scale.z);
		}

		public static Matrix4 GetRotateX(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix4(1, 0, 0, 0, 0, cos, sin, 0, 0, -sin, cos, 0, 0, 0, 0, 1);
		}

		public static Matrix4 GetRotateY(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix4(cos, 0, -sin, 0, 0, 1, 0, 0, sin, 0, cos, 0, 0, 0, 0, 1);
		}

		public static Matrix4 GetRotateZ(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix4(cos, sin, 0, 0, -sin, cos, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
		}

		public static Matrix4 GetTranslation(Vector3 pos)
		{
			return new Matrix4(m03: pos.x, m13: pos.y, m23: pos.z);
		}

		public static Matrix4 GetTranslation(float x, float y, float z)
		{
			return new Matrix4(m02: x, m12: y, m23: z);
		}

		public Vector3 GetTranslation()
		{
			return new Vector3(m[3], m[7], m[11]);
		}

		
		//public Matrix4 Inverse()
		//{
		//	// 0   4   8   12
		//	// 1   5   9   13
		//	// 2   6   10  14
		//	// 3   7   11  15

		//	//det 0
		//	float d5TL = m[10] * m[15] - m[14] * m[11];
		//	float d9TL = m[6] * m[15] - m[14] * m[7]; 
		//	float d13TL = m[6] * m[11] - m[10] * m[7];

		//	float det0 = m[5] * m[10] * m[15] + m[6] ;
		//		//m[5] * d5TL - m[9] * d9TL + m[13] * d13TL;

		//	//det 4
		//	float d1ML = m[9] * m[14] - m[13] * m[10];
		//	float d9ML = m[1] * m[14] - m[13] * m[2];
		//	float d13ML = m[1] * m[10] - m[9] * m[2];

		//	float det8 = ;
		//		//m[1] * d1ML - m[9] * d9ML + m[13] * d13ML;

		//	//det 8
		//	float d1MR = m[5] * m[14] - m[13] * m[6];
		//	float d5MR = m[1] * m[14] - m[13] * m[2];
		//	float d13MR = m[1] * m[6] - m[5] * m[2];

		//	float det12 = m[1] * d1MR - m[5] * d5MR + m[13] * d13MR;

		//	//det 12
		//	float d1TR = m[6] * m[11] - m[10] * m[7]; //equal to d13TL
		//	float d5TR = m[2] * m[11] - m[10] * m[3];
		//	float d9TR = m[2] * m[7] - m[6] * m[3];

		//	float det4 = m[1] * d1TR - m[5] * d5TR + m[9] * d9TR;



		//	//det full
		//	float det = m[0] * det0 - m[4] * det4 + m[8] * det8 - m[12] * det12;

		//	Console.WriteLine(det);
		//	Matrix4 i = Identity;
		//	if (det == 0)
		//		return i; // THERE IS NO INVERSE
		//	return i;
		//	float iDet = 1 / det;

		//}

	}
}
