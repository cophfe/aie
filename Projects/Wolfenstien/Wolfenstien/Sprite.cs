using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.IO;

namespace Wolfenstien
{
    enum SpriteType
    {
        Enemy,
        Lamp,
        Length
    }
    enum AnimatedSpriteType
    {
        EnemyDancing = SpriteType.Length
    }
    class Sprite : Texture
    {
        Vector2 position;
        
        public Sprite(SpriteType spriteType, Vector2 position) : base(GetBitmapFromSpriteType(spriteType),true)
        {
            this.position = position;
                
        }

        static public Bitmap GetBitmapFromSpriteType(SpriteType sT)
        {
            
            return new Bitmap(@$"{Directory.GetCurrentDirectory()}\Images\Sprites\{sT}.png");
        }
        
    }
}
