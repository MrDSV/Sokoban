using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models
{
    class Player
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private Point moveToDirect;
        private Point playerFrame;

        public Texture2D Texture
        {
            get
            { return texture; }
            set
            { texture = value; }
        }

        public Vector2 Position
        {
            get
            { return position; }
            set
            { position = value; }
        }

        public Rectangle Rectangle
        {
            get
            { return rectangle; }
            set
            { rectangle = value; }
        }

        public Point MoveToDirect
        {
            get
            { return moveToDirect; }
            set
            { moveToDirect = value; }
        }

        public Point PlayerFrame
        {
            get
            { return playerFrame; }
            set
            { playerFrame = value; }
        }

        public Player()
        {
            position = new Vector2(2, 2);
            moveToDirect = new Point(0, 0);
            playerFrame = new Point(0, 0);

        }

        public void Load(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>("Player");
        }

        public void Draw(SpriteBatch spriteBatch, int dx, int dy, int animate)
        {
            spriteBatch.Draw(texture,
                new Vector2(position.X * 32 + dx, position.Y * 32 + dy),
                new Rectangle((playerFrame.X + animate / 5) * 64, playerFrame.Y * 64, 64, 64),
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(0.5f, 0.5f),
                SpriteEffects.None, 0);
        }
    }
}

