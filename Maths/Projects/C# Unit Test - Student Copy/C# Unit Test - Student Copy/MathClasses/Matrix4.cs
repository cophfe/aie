using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public struct Matrix4
	{
		public float m1, m2, m3, m4,
					 m5, m6, m7, m8,
					 m9, m10, m11, m12,
					 m13, m14, m15, m16;

		public Matrix4(float m00 = 1, float m01 = 0, float m02 = 0, float m03 = 0,
						float m10 = 0, float m11 = 1, float m12 = 0, float m13 = 0,
						float m20 = 0, float m21 = 0, float m22 = 1, float m23 = 0,
						float m30 = 0, float m31 = 0, float m32 = 0, float m33 = 1)
		{
			this.m1 = m00;
			this.m2 = m01;
			this.m3 = m02;
			this.m4 = m03;
			this.m5 = m10;
			this.m6 = m11;
			this.m7 = m12;
			this.m8 = m13;
			this.m9 = m20;
			this.m10 = m21;
			this.m11 = m22;
			this.m12 = m23;
			this.m13 = m30;
			this.m14 = m31;
			this.m15 = m32;
			this.m16 = m33;
		}

		public Matrix4 Identity
		{
			get {
				return new Matrix4(1, 0, 0, 0,
								  0, 1, 0, 0,
								  0, 0, 1, 0,
								  0, 0, 0, 1); 
			}
		}

		public Matrix4 Zero
		{
			get { return new Matrix4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); }
		}

		public static Vector4 operator *(Matrix4 m, Vector4 v)
		{
			return new Vector4(m.m1 * v.x + m.m5 * v.y + m.m9 * v.z + m.m13 * v.w,
								m.m2 * v.x + m.m6 * v.y + m.m10 * v.z + m.m14 * v.w,
								m.m3 * v.x + m.m7 * v.y + m.m11 * v.z + m.m15 * v.w,
								m.m4 * v.x + m.m8 * v.y + m.m12 * v.z + m.m16 * v.w);
		}

		//cannot multiply vector by matrix
		//(i mean, we can, but it would really be multiplying the matrix by vector)

		public static Matrix4 operator *(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m1 * b.m1 + a.m2 * b.m5 + a.m3 * b.m9 + a.m4 * b.m13,   //m00
								a.m1 * b.m2 + a.m2 * b.m6 + a.m3 * b.m10 + a.m4 * b.m14,  //m01
								a.m1 * b.m3 + a.m2 * b.m7 + a.m3 * b.m11 + a.m4 * b.m15,  //m02
								a.m1 * b.m4 + a.m2 * b.m8 + a.m3 * b.m12 + a.m4 * b.m16,	//m03
								a.m5 * b.m1 + a.m6 * b.m5 + a.m7 * b.m9 + a.m8 * b.m13,  //m10
								a.m5 * b.m2 + a.m6 * b.m6 + a.m7 * b.m10 + a.m8 * b.m14,  //m11
								a.m5 * b.m3 + a.m6 * b.m7 + a.m7 * b.m11 + a.m8 * b.m15,  //m12
								a.m5 * b.m4 + a.m6 * b.m8 + a.m7 * b.m12 + a.m8 * b.m16,  //m13
								a.m9 * b.m1 + a.m10 * b.m5 + a.m11 * b.m9 + a.m12 * b.m13,  //m20
								a.m9 * b.m2 + a.m10 * b.m6 + a.m11 * b.m10 + a.m12 * b.m14,  //m21
								a.m9 * b.m3 + a.m10 * b.m7 + a.m11 * b.m11 + a.m12 * b.m15,	//m22
								a.m9 * b.m4 + a.m10 * b.m8 + a.m11 * b.m12 + a.m12 * b.m16,  //m23
								a.m13 * b.m1 + a.m14 * b.m5 + a.m15 * b.m9 + a.m16 * b.m13,  //m30
								a.m13 * b.m2 + a.m14 * b.m6 + a.m15 * b.m10 + a.m16 * b.m14,  //m31
								a.m13 * b.m3 + a.m14 * b.m7 + a.m15 * b.m11 + a.m16 * b.m15,  //m32
								a.m13 * b.m4 + a.m14 * b.m8 + a.m15 * b.m12 + a.m16 * b.m16); //m33
		}

		public static Matrix4 operator +(Matrix4 m, float f)
		{
			return new Matrix4(m.m1 + f, m.m2 + f, m.m3 + f, m.m4 + f,
								m.m5 + f, m.m6 + f, m.m7 + f, m.m8 + f,
								m.m9 + f, m.m10 + f, m.m11 + f, m.m12 + f,
								m.m13 + f, m.m14 + f, m.m15 + f, m.m16 + f);
		}

		public static Matrix4 operator +(float f, Matrix4 m)
		{
			return new Matrix4(m.m1 + f, m.m2 + f, m.m3 + f, m.m4 + f,
								m.m5 + f, m.m6 + f, m.m7 + f, m.m8 + f,
								m.m9 + f, m.m10 + f, m.m11 + f, m.m12 + f,
								m.m13 + f, m.m14 + f, m.m15 + f, m.m16 + f);
		}

		public static Matrix4 operator +(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m1 + b.m1, a.m2 + b.m2, a.m3 + b.m3, a.m4 + b.m4,
								a.m5 + b.m5, a.m6 + b.m6, a.m7 + b.m7, a.m8 + b.m8,
								a.m9 + b.m9, a.m10 + b.m10, a.m11 + b.m11, a.m12 + b.m12,
								a.m9 + b.m9, a.m10 + b.m10, a.m11 + b.m11, a.m16 + b.m16);
		}
		public static Matrix4 operator -(Matrix4 a, Matrix4 b)
		{
			return new Matrix4(a.m1 - b.m1, a.m2 - b.m2, a.m3 - b.m3, a.m4 - b.m4,
								a.m5 - b.m5, a.m6 - b.m6, a.m7 - b.m7, a.m8 - b.m8,
								a.m9 - b.m9, a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12,
								a.m9 - b.m9, a.m10 - b.m10, a.m11 - b.m11, a.m16 - b.m16);
		}

		public void SetRotateX(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m1 = 1; m2 = 0;		m3 = 0;		m4 = 0;	   
			m5 = 0; m6 = cos;	m7 = sin;	m8 = 0;	   
			m9 = 0; m10 = -sin;	m11 = cos;	m12 = 0;   
			m13 = 0;m14 = 0;	m15 = 0;	m16 = 1;   
		}

		public void SetRotateY(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m1 = cos;	m2 = 0;		m3 = -sin;	m4 = 0;	 
			m5 = 0;		m6 = 1;		m7 = 0;		m8 = 0;	 
			m9 = sin;	m10 = 0;	m11 = cos;	m12 = 0; 
			m13 = 0;	m14 = 0;	m15 = 0;	m16 = 1; 
		}

		public void SetRotateZ(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m1 = cos;	m2 = sin;	m3 = 0;		m4 = 0;
			m5 = -sin;	m6 = cos;	m7 = 0;		m8 = 0;
			m9 = 0;		m10 = 0;	m11 = 1;	m12 = 0;
			m13 = 0; m14 = 0; m15 = 0; m16 = 1;	m16 = 1;
		}


	}
}
