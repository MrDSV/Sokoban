using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Tiles
    {
        protected Texture2D texture;
        private Rectangle rectangle;
        private static ContentManager content;

        public Rectangle Rectangle
        {
            get
            { return rectangle; }
            set
            { rectangle = value; }
        }

        public static ContentManager Content
        {
            get
            { return content; }
            set
            { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class TilesLoader : Tiles
    {
        public TilesLoader(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Tile" + i);
            Rectangle = newRectangle;
        }
    }
}

