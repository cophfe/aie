using System;
using System.Collections.Generic;
using System.Text;

namespace Cube
{
    class Vector2
    {
        public float x,y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        
        public bool Equal(float x, float y)
        {
            if (x == this.x && y == this.y)
                return true;
            else
                return false;
        }

    }
}
