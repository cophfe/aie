using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleImproved
{
    struct V3
    {
        float x, y, z;
        public V3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
       

        public float Length
        {
            get
            {
                return MathF.Sqrt(x * x + y * y + z * z);
            }
            set
            {
                float l = Length;
                if (l > 0)
                {
                    float mag = value / l;
                    x *= mag;
                    y *= mag;
                    z *= mag;
                }
            }
        }

        public V3 Normalised
        {
            get
            {
                float l = Length;
                if (l > 0) //dont divide by zero fools 
                {
                    // to get a normalized vector divide each axis by the length of the vector
                    float invL = 1 / l;
                    x *= invL;
                    y *= invL;
                    z *= invL;
                }

                return this;
            }
            
            
        }

        public static float Dot(V3 a, V3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static V3 Cross(V3 a, V3 b)
        {
            return new V3(a.y * b.z - a.z * b.y, a.z*b.x - a.x*b.z, a.x * b.y- a.y*b.x);
        }
        public static float AngleBetween(V3 a, V3 b)
        {
            return MathF.Acos(Dot(a.Normalised, b.Normalised));
        }

        public void setLength(float newLength)
        {
            float l = Length;
            if (l > 0)
            {
                float mag = newLength / l;
                x *= mag;
                y *= mag;
                z *= mag;
            }
        }

        public static V3 operator +(V3 a, V3 b)
        {
            return new V3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static V3 operator -(V3 a, V3 b)
        {
            return new V3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static V3 operator * (V3 a, float b)
        {
            return new V3(a.x * b, a.y * b, a.z * b);
        }
        public static V3 operator /(V3 a, float b)
        {
            float bb = 1 / b;
            return new V3(a.x * bb, a.y * bb, a.z * bb);
        }

    }

    
}
