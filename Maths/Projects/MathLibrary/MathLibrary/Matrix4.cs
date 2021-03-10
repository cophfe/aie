using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mlib
{
	public struct Matrix4
	{
		public float m00, m01, m02, m03,
					 m10, m11, m12, m13,
					 m20, m21, m22, m23,
					 m30, m31, m32, m33;

		public Matrix4(float m00, float m01, float m02, float m03,
						float m10, float m11, float m12, float m13,
						float m20, float m21, float m22, float m23,
						float m30, float m31, float m32, float m33)
		{
			this.m00 = m00;
			this.m01 = m01;
			this.m02 = m02;
			this.m03 = m03;
			this.m10 = m10;
			this.m11 = m11;
			this.m12 = m12;
			this.m13 = m13;
			this.m20 = m20;
			this.m21 = m21;
			this.m22 = m22;
			this.m23 = m23;
			this.m30 = m30;
			this.m31 = m31;
			this.m32 = m32;
			this.m33 = m33;
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
			return new Vector4(m.m00 * v.x + m.m10 * v.y + m.m20 * v.z + m.m30 * v.w,
								m.m01 * v.x + m.m11 * v.y + m.m21 * v.z + m.m31 * v.w,
								m.m02 * v.x + m.m12 * v.y + m.m22 * v.z + m.m32 * v.w,
								m.m03 * v.x + m.m13 * v.y + m.m23 * v.z + m.m33 * v.w);
		}

		//cannot multiply vector by matrix
		//(i mean, we can, but it would really be multiplying the matrix by vector)

		public static Matrix4 operator *(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20 + a.m03 * b.m30,   //m00
								a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21 + a.m03 * b.m31,  //m01
								a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22 + a.m03 * b.m32,  //m02
								a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03 * b.m33,	//m03
								a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20 + a.m13 * b.m30,  //m10
								a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31,  //m11
								a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32,  //m12
								a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33,  //m13
								a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20 + a.m23 * b.m30,  //m20
								a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31,  //m21
								a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32,	//m22
								a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33,  //m23
								a.m30 * b.m00 + a.m31 * b.m10 + a.m32 * b.m20 + a.m33 * b.m30,  //m30
								a.m30 * b.m01 + a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31,  //m31
								a.m30 * b.m02 + a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32,  //m32
								a.m30 * b.m03 + a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33); //m33
		}

		public static Matrix4 operator +(Matrix4 m, float f)
		{
			return new Matrix4(m.m00 + f, m.m01 + f, m.m02 + f, m.m03 + f,
								m.m10 + f, m.m11 + f, m.m12 + f, m.m13 + f,
								m.m20 + f, m.m21 + f, m.m22 + f, m.m23 + f,
								m.m30 + f, m.m31 + f, m.m32 + f, m.m33 + f);
		}

		public static Matrix4 operator +(float f, Matrix4 m)
		{
			return new Matrix4(m.m00 + f, m.m01 + f, m.m02 + f, m.m03 + f,
								m.m10 + f, m.m11 + f, m.m12 + f, m.m13 + f,
								m.m20 + f, m.m21 + f, m.m22 + f, m.m23 + f,
								m.m30 + f, m.m31 + f, m.m32 + f, m.m33 + f);
		}

		public static Matrix4 operator +(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m00 + b.m00, a.m01 + b.m01, a.m02 + b.m02, a.m03 + b.m03,
								a.m10 + b.m10, a.m11 + b.m11, a.m12 + b.m12, a.m13 + b.m13,
								a.m20 + b.m20, a.m21 + b.m21, a.m22 + b.m22, a.m23 + b.m23,
								a.m20 + b.m20, a.m21 + b.m21, a.m22 + b.m22, a.m33 + b.m33);
		}
		public static Matrix4 operator -(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02, a.m03 - b.m03,
								a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12, a.m13 - b.m13,
								a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22, a.m23 - b.m23,
								a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22, a.m33 - b.m33);
		}

		public void SetRotateX(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m00 = 1; m01 = 0;		m02 = 0;	m03 = 0;  
			m10 = 0; m11 = cos;		m12 = sin;	m13 = 0;  
			m20 = 0; m21 = -sin;	m22 = cos;	m23 = 0;  
			m30 = 0; m31 = 0;		m32 = 0;	m33 = 1;  
		}

		public void SetRotateY(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m00 = cos;	m01 = 0;	m02 = -sin;	m03 = 0;  
			m10 = 0;	m11 = 1;	m12 = 0;	m13 = 0;  
			m20 = sin;	m21 = 0;	m22 = cos;	m23 = 0;  
			m30 = 0;	m31 = 0;	m32 = 0;	m33 = 1;
		}

		public void SetRotateZ(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m00 = cos;	m01 = sin;	m02 = 0;	m03 = 0;
			m10 = -sin; m11 = cos;	m12 = 0;	m13 = 0;
			m20 = 0;	m21 = 0;	m22 = 1;	m23 = 0;
			m30 = 0;	m31 = 0;	m32 = 0;	m33 = 1;
		}


	}
}
