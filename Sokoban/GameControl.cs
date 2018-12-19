using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sokoban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class GameControl
    {
        private Keys previousKey;
        private int delayMove;
        private int animationLoop;
        private int animate;

        enum Cell
        {
            Empty,
            Ground,
            Wall,
            Place,
            Box,
            BoxOnPlace
        }

        public Keys PreviousKey
        {
            get
            { return previousKey; }
            set
            { previousKey = value; }
        }
        public int DelayMove
        {
            get
            { return delayMove; }
            set
            { delayMove = value; }
        }
        public int AnimationLoop
        {
            get
            { return animationLoop; }
            set
            { animationLoop = value; }
        }

        public int Animate
        {
            get
            { return animate; }
            set
            { animate = value; }
        }

        public GameControl()
        {
            previousKey = 0;
            delayMove = 0;
        }

        public Point CheckPressKey(GameTime gameTime)
        {
            var point = new Point(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                previousKey = Keys.Right;
                point.X = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                previousKey = Keys.Left;
                point.X = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                previousKey = Keys.Up;
                point.Y = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                previousKey = Keys.Down;
                point.Y = 1;
            }
            return point;
        }

        public Point MoveControl(Point direct, Point position, Level level)
        {
            var offset = new Point(0, 0);
            if (direct.X == 0 && direct.Y == 0)
                return offset;

            if (direct.X == -1 && position.X > 0 && level.Map[position.Y, position.X - 1] != (int)Cell.Wall)
                offset.X = -1;
            else if (direct.X == 1 && position.X < level.Heigth && level.Map[position.Y, position.X + 1] != (int)Cell.Wall)
                offset.X = 1;
            else if (direct.Y == -1 && position.Y > 0 && level.Map[position.Y - 1, position.X] != (int)Cell.Wall)
                offset.Y = -1;
            else if (direct.Y == 1 && position.Y < level.Width && level.Map[position.Y + 1, position.X] != (int)Cell.Wall)
                offset.Y = 1;
            return offset;
        }
    }
}
