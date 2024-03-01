using GXPEngine;
using TiledMapParser;

    public class Box : AnimationSprite
    {
        public int type;
        public Box(string filename, int colls, int rows, TiledObject obg = null) : base(filename, colls, rows)
        {
            type = obg.GetIntProperty("type");
            // switch (type)
            // {
            //     case 0: color = 0xFF00FF;
            //         break;
            //     case 1: color = 0xFF0000;
            //         break;
            //     case 2: color = 0x00FF00;
            //         break;
            //     case 3: color = 0x0000FF;
            //         break;
            // }
        }

        public int getBoxType()
        {
            return type;
        }
    }
