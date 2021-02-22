using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace FirstPerson
{
    class Player
    {
        Vector2 pPos;
        Vector2 pAngle;
        public Player(Vector2 pPos, Vector2 pAngle)
        {
            this.pPos = pPos;
            this.pAngle = pAngle;
        }

        public void Rotate(float amount)
        {

        }
    }
}
