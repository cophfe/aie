using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public struct Matrix3
	{
		public float m00, m01, m02,
				     m10, m11, m12,
				     m20, m21, m22;

		public Matrix3 (float m00, float m01, float m02,
						float m10, float m11, float m12,
						float m20, float m21, float m22)
		{
			this.m00 = m00;
			this.m01 = m01;
			this.m02 = m02;
			this.m10 = m10;
			this.m11 = m11;
			this.m12 = m12;
			this.m20 = m20;
			this.m21 = m21;
			this.m22 = m22;
		}

		public Matrix3 Identity
		{
			get { return new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1); }
		}

		public Matrix3 Zero
		{
			get { return new Matrix3(0, 0, 0, 0, 0, 0, 0, 0, 0); }
		}

		public static Vector3 operator *(Matrix3 m, Vector3 v)
		{
			return new Vector3(m.m00 * v.x + m.m10 * v.y + m.m20 * v.z,
								m.m01 * v.x + m.m11 * v.y + m.m21 * v.z,
								m.m02 * v.x + m.m12 * v.y + m.m22 * v.z);
		}

		//cannot multiply vector by matrix
		//(i mean, we can, but it would really be multiplying the matrix by vector)

		public static Matrix3 operator *(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20,	//m00
								a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21,  //m01
								a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22,  //m02
								a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20,  //m10
								a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21,  //m11
								a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22,  //m12
								a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20,  //m20
								a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21,  //m21
								a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22); //m22
		}

		public static Matrix3 operator +(Matrix3 m, float f)
		{
			return new Matrix3(m.m00 + f, m.m01 + f, m.m02 + f,
								m.m10 + f, m.m11 + f, m.m12 + f,
								m.m20 + f, m.m21 + f, m.m22 + f);
		}

		public static Matrix3 operator +(float f, Matrix3 m)
		{
			return new Matrix3(m.m00 + f, m.m01 + f, m.m02 + f,
								m.m10 + f, m.m11 + f, m.m12 + f,
								m.m20 + f, m.m21 + f, m.m22 + f);
		}

		public static Matrix3 operator +(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m00 + b.m00, a.m01 + b.m01, a.m02 + b.m02,
								a.m10 + b.m10, a.m11 + b.m11, a.m12 + b.m12,
								a.m20 + b.m20, a.m21 + b.m21, a.m22 + b.m22);
		}
		public static Matrix3 operator -(Matrix3 a, Matrix3 b)
		{
			return new Matrix3(a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02,
								a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12,
								a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22);
		}

		public void SetRotateX( float angle )
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m00 = 0; m01 = 0; m02 = 0;
			m10 = 0; m11 = cos; m12 = sin;
			m20 = 0; m21 = -sin; m22 = cos;
		}

		public void SetRotateY(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m00 = cos; m01 = 0; m02 = -sin;
			m10 = 0; m11 = 0; m12 = 0;
			m20 = sin; m21 = 0; m22 = cos;
		}

		public void SetRotateZ(float angle)
		{
			float sin = (float)Math.Sin(angle);
			float cos = (float)Math.Cos(angle);

			m00 = cos; m01 = sin; m02 = 0;
			m10 = -sin; m11 = cos; m12 = 0;
			m20 = 0; m21 = 0; m22 = 0;
		}


	}
}