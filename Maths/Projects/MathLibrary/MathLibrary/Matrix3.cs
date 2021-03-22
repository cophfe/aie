using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mlib
{
	public struct Matrix3
	{

		public float m11, m21, m31,
						m12, m22, m32,
						m13, m23, m33;

		public Matrix3(float m11 = 1, float m21 = 0, float m31 = 0,
						float m12 = 0, float m22 = 1, float m32 = 0,
						float m13 = 0, float m23 = 0, float m33 = 1)
		{
			this.m11 = m11;
			this.m12 = m12;
			this.m13 = m13;
			this.m21 = m21;
			this.m22 = m22;
			this.m23 = m23;
			this.m31 = m31;
			this.m32 = m32;
			this.m33 = m33;
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
			return new Vector3(m.m11 * v.x + m.m12 * v.y + m.m13 * v.z,
								m.m21 * v.x + m.m22 * v.y + m.m23 * v.z,
								m.m31 * v.x + m.m32 * v.y + m.m33 * v.z);
		}

		public static Vector2 operator *(Matrix3 m, Vector2 v)
		{
			return new Vector2(m.m11 * v.x + m.m12 * v.y,
								m.m21 * v.x + m.m32 * v.y);
		}

		public static Matrix3 operator *(Matrix3 a, Matrix3 b)
		{
			return new Matrix3((a.m11 * b.m11) + (a.m12 * b.m21) + (a.m13 * b.m31),     //m11
								(a.m21 * b.m11) + (a.m22 * b.m21) + (a.m23 * b.m31),    //m21
								(a.m31 * b.m11) + (a.m32 * b.m21) + (a.m33 * b.m31),    //m31
								(a.m11 * b.m12) + (a.m12 * b.m22) + (a.m13 * b.m32),    //m12 
								(a.m21 * b.m12) + (a.m22 * b.m22) + (a.m23 * b.m32),    //m22
								(a.m31 * b.m12) + (a.m32 * b.m22) + (a.m33 * b.m32),    //m32 
								(a.m11 * b.m13) + (a.m12 * b.m23) + (a.m13 * b.m33),    //m13
								(a.m21 * b.m13) + (a.m22 * b.m23) + (a.m23 * b.m33),    //m23
								(a.m31 * b.m13) + (a.m32 * b.m23) + (a.m33 * b.m33));   //m33
		}

		public static Matrix3 operator +(Matrix3 m, float f)
		{
			return new Matrix3(m.m11 + f, m.m21 + f, m.m31 + f,
								m.m12 + f, m.m22 + f, m.m32 + f,
								m.m13 + f, m.m23 + f, m.m33 + f);
		}

		public static Matrix3 operator +(float f, Matrix3 m)
		{
			return new Matrix3(m.m11 + f, m.m21 + f, m.m31 + f,
								m.m12 + f, m.m22 + f, m.m32 + f,
								m.m13 + f, m.m23 + f, m.m33 + f);
		}

		public static Matrix3 operator +(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m11 + b.m11, a.m21 + b.m21, a.m31 + b.m31,
								a.m12 + b.m12, a.m22 + b.m22, a.m32 + b.m32,
								a.m13 + b.m13, a.m23 + b.m23, a.m33 + b.m33);
		}
		public static Matrix3 operator -(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m11 - b.m11, a.m21 - b.m21, a.m31 - b.m31,
								a.m12 - b.m12, a.m22 - b.m22, a.m32 - b.m32,
								a.m13 - b.m13, a.m23 - b.m23, a.m33 - b.m33);
		}

		public void SetRotateX(float angle)
		{


			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m11 = 1; m12 = 0; m13 = 0;
			m21 = 0; m22 = cos; m23 = -sin;
			m31 = 0; m32 = sin; m33 = cos;
		}

		public void SetRotateY(float angle)
		{

			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m11 = cos; m12 = 0; m13 = sin;
			m21 = 0;   m22 = 1; m23 = 0;
			m31 = -sin; m32 = 0; m33 = cos;
		}

		public void SetRotateZ(float angle)
		{

			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m11 = cos; m12 = -sin; m13 = 0;
			m21 = sin; m22 = cos; m23 = 0;
			m31 = 0; m32 = 0; m33 = 1;
		}

		public Matrix3 Inverse()
		{

			float d0 = m22 * m33 - m32 * m23, d3 = m21 * m33 - m23 * m31, d6 = m21 * m32 - m22 * m31;
			float det = m11 * d0 - m12 * d3 + m13 * d6;
			Matrix3 i = Identity;
			if (det == 0)
				return i; // THERE IS NO INVERSE

			//transposes, cofactors minor-ises and divides by the determinant all in one step (god, please dont make me debug this)

			
			float iDet = 1/ det;
			i.m11 = d0 * iDet;
			i.m21 = -d3 * iDet;
			i.m31 = d6 * iDet;
			i.m12 = -(m12 * m33 - m13 * m32) * iDet;
			i.m22 = (m11 * m33 - m13 * m31) * iDet;
			i.m32 = -(m11 * m32 - m12 * m31) * iDet;
			i.m13 = (m12 * m23 - m13 * m22) * iDet;
			i.m23 = -(m11 * m23 - m13 * m21) * iDet;
			i.m33 = (m11 * m22 - m12 * m21) * iDet;

			return i;

		}


		public void SetTranslation(float x, float y)
		{
			m13 = x;
			m23 = y;
			m33 = 1;
		}

		public void SetTranslation(Vector2 pos)
		{
			m13 = pos.x;
			m23 = pos.y;
			m33 = 1;
		}


		public void SetScale(float x, float y)
		{
			m11 = x; m21 = 0; m31 = 0; 
			m12 = 0; m22 = y; m32 = 0; 
			m13 = 0; m23 = 0; m33 = 1;
		}

		public void SetScale(Vector2 scale)
		{
			m11 = scale.x; m21 = 0;		m31 = 0;
			m12 = 0;		m22 = scale.y; m32 = 0;
			m13 = 0;		m23 = 0;		m33 = 1;
		}

		public static Matrix3 GetScale(float x, float y)
		{
			return new Matrix3(x, m22: y);
		}

		public static Matrix3 GetScale(Vector2 scale)
		{
			return new Matrix3(scale.x, m22: scale.y);
		}

		public static Matrix3 GetRotateX(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix3(1, 0, 0,
								0, cos, -sin,
								0, sin, cos);
		}

		public static Matrix3 GetRotateY(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix3(cos, 0, sin,
								0, 1, 0, 
								-sin, 0, cos);
		}

		public static Matrix3 GetRotateZ(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			return new Matrix3(cos, -sin, 0,
								sin, cos, 0,
								0, 0, 1);
		}

		public static Matrix3 GetTranslation(Vector2 pos)
		{
			return new Matrix3(m13: pos.x, m23: pos.y);
		}

		public static Matrix3 GetTranslation(float x, float y)
		{
			return new Matrix3(m13: x, m23: y);
		}

		public Vector2 GetTranslation()
		{
			return new Vector2(m13, m23);
		}
	}
}
