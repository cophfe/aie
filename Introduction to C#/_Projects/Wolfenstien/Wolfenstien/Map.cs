using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace Wolfenstien
{
    
    enum CoordValue
    {
        Sprite = -5,
        PlayerStart, // anything more than 0 is detected as a renderable wall
        Exit,
        SecretExit,
        HiddenArea,
        Nothing,
        StoneWall,
        WoodenWall,
        DefaultWall,
        BlueWall,
        Length
        
    }
    class Map //Map is currently flipped on vertical axis (i think?) for some reason
    {
        List<Sprite> spriteList = new List<Sprite>();
        CoordValue[,] map;
        Vector2 playerStart;
        List<Sprite> sprites;
        public Map(Bitmap bitmap)
        {
            map = new CoordValue[bitmap.Width, bitmap.Height];
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Color clr = bitmap.GetPixel(bitmap.Width-x -1, y);
                    int value = clr.R;
                    switch (value)
                    {
                        case 0:
                            map[x, y] = CoordValue.Nothing; //Black
                            break;
                        case 128:
                            map[x, y] = CoordValue.StoneWall; //Grey
                            break;
                        case 20:
                            map[x, y] = CoordValue.BlueWall; //Blue
                            break;
                        case 240:
                            map[x, y] = CoordValue.WoodenWall; //Yellow
                            break;
                        case 255:
                            map[x, y] = CoordValue.Exit; //Red
                            break;
                        case 30:
                            map[x, y] = CoordValue.SecretExit; //Green
                            break;
                        case 40:
                            map[x, y] = CoordValue.HiddenArea; //Cyan
                            break;
                        case 250:
                            map[x, y] = CoordValue.Nothing; //Purple
                            playerStart = new Vector2(x + 0.5f, y + 0.5f);
                            break;
                        case 65:
                            map[x, y] = CoordValue.Nothing;
                            sprites.Add(new Sprite(SpriteType.Enemy, new Vector2(x + 0.5f, y + 0.5f)));
                            break;
                        default:
                            map[x, y] = CoordValue.DefaultWall; //Anything Else (needs to be a different wall for debugging)
                            break;

                    }
                }
            }
        }

        public Vector2 PlayerStart
        {
            get
            {
                return playerStart;
            }
        }
        public CoordValue[,] MapValues
        {
            get
            {
                return map;
            }
            
        }
        public List<Sprite> GetSprites()
        {
            return spriteList;
        }
    }
}
