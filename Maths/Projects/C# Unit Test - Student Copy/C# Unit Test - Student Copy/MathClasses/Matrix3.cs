using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MathClasses
{
    public struct Matrix3
	{
		public float m1, m2, m3,
				     m4, m5, m6,
				     m7, m8, m9;

		public Matrix3 (float m00 = 1, float m01 = 0, float m02 = 0,
						float m10 = 0, float m11 = 1, float m12 = 0,
						float m20 = 0, float m21 = 0, float m22 = 1)
		{
			this.m1 = m00;
			this.m2 = m01;
			this.m3 = m02;
			this.m4 = m10;
			this.m5 = m11;
			this.m6 = m12;
			this.m7 = m20;
			this.m8 = m21;
			this.m9 = m22;
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
			return new Vector3(m.m1 * v.x + m.m4 * v.y + m.m7 * v.z,
								m.m2 * v.x + m.m5 * v.y + m.m8 * v.z,
								m.m3 * v.x + m.m6 * v.y + m.m9 * v.z);
		}

		//cannot multiply vector by matrix
		//(i mean, we can, but it would really be multiplying the matrix by vector)

		public static Matrix3 operator *(Matrix3 b, Matrix3 a)
		{
			return new Matrix3(a.m1 * b.m1 + a.m2 * b.m4 + a.m3 * b.m7,	//m00
								a.m1 * b.m2 + a.m2 * b.m5 + a.m3 * b.m8,  //m01
								a.m1 * b.m3 + a.m2 * b.m6 + a.m3 * b.m9,  //m02
								a.m4 * b.m1 + a.m5 * b.m4 + a.m6 * b.m7,  //m10
								a.m4 * b.m2 + a.m5 * b.m5 + a.m6 * b.m8,  //m11
								a.m4 * b.m3 + a.m5 * b.m6 + a.m6 * b.m9,  //m12
								a.m7 * b.m1 + a.m8 * b.m4 + a.m9 * b.m7,  //m20
								a.m7 * b.m2 + a.m8 * b.m5 + a.m9 * b.m8,  //m21
								a.m7 * b.m3 + a.m8 * b.m6 + a.m9 * b.m9); //m22
		}

		public static Matrix3 operator +(Matrix3 m, float f)
		{
			return new Matrix3(m.m1 + f, m.m2 + f, m.m3 + f,
								m.m4 + f, m.m5 + f, m.m6 + f,
								m.m7 + f, m.m8 + f, m.m9 + f);
		}

		public static Matrix3 operator +(float f, Matrix3 m)
		{
			return new Matrix3(m.m1 + f, m.m2 + f, m.m3 + f,
								m.m4 + f, m.m5 + f, m.m6 + f,
								m.m7 + f, m.m8 + f, m.m9 + f);
		}

		public static Matrix3 operator +(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m1 + b.m1, a.m2 + b.m2, a.m3 + b.m3,
								a.m4 + b.m4, a.m5 + b.m5, a.m6 + b.m6,
								a.m7 + b.m7, a.m8 + b.m8, a.m9 + b.m9);
		}
		public static Matrix3 operator -(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m1 - b.m1, a.m2 - b.m2, a.m3 - b.m3,
								a.m4 - b.m4, a.m5 - b.m5, a.m6 - b.m6,
								a.m7 - b.m7, a.m8 - b.m8, a.m9 - b.m9);
		}

		public void SetRotateX( float angle )
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m1 = 1; m2 = 0; m3 = 0;
			m4 = 0; m5 = cos; m6 = sin;
			m7 = 0; m8 = -sin; m9 = cos;
		}

		public void SetRotateY(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m1 = cos; m2 = 0; m3 = -sin;
			m4 = 0; m5 = 1; m6 = 0;
			m7 = sin; m8 = 0; m9 = cos;
		}

		public void SetRotateZ(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m1 = cos; m2 = sin; m3 = 0;
			m4 = -sin; m5 = cos; m6 = 0;
			m7 = 0; m8 = 0; m9 = 1;
		}


	}
}