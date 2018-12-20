using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban.Models;
using System;
using System.IO;

namespace Sokoban
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        const int sizeTile = 32;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameState gameState = new GameState();
        GameControl gameControl = new GameControl();
        Scenes scene = new Scenes();
        Level level = new Level();
        Player player = new Player();
        Point delta = new Point(0, 0);
        // int animate = 0;

        enum Cell
        {
            Empty,
            Ground,
            Wall,
            Place,
            Box,
            BoxOnPlace
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameState.IsLvlComplete = false;
            gameControl = new GameControl();
            try
            {
                StreamReader fileLevels = new StreamReader("./levels/level" + gameState.LvlNumber + ".txt");
                string str = fileLevels.ReadLine();
                var temp = str.Split(' ');
                level.Width = int.Parse(temp[0]);
                level.Heigth = int.Parse(temp[1]);
                str = fileLevels.ReadLine();
                temp = str.Split(' ');
                player.Position = new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                var lvlMap = new int[level.Width, level.Heigth];
                for (int i = 0; i < level.Width; i++)
                {
                    str = fileLevels.ReadLine();
                    temp = str.Split(' ');
                    for (int j = 0; j < level.Heigth; j++)
                    {
                        lvlMap[i, j] = int.Parse(temp[j]);
                    }
                }
                fileLevels.Close();
                level.Map = lvlMap;
                gameState.CountSteps = 0;
                player.MoveToDirect = new Point(0, 0);

                graphics.PreferredBackBufferWidth = level.Heigth * sizeTile;
                graphics.PreferredBackBufferHeight = level.Width * sizeTile;
                graphics.ApplyChanges();
            }
            catch { gameState.IsGameOver = true; }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Tiles.Content = Content;
            player.Load(Content);
            scene.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (!gameState.IsGameStart)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState.IsGameStart = true;
                }
                return;
            }

            if (gameState.IsLvlComplete)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gameState.LvlNumber++;
                    Initialize();
                }
                return;
            }

            gameState.IsLvlComplete = gameState.IsLevelComplete(level);
            mapUpdate(gameTime);
            base.Update(gameTime);
        }


        private void mapUpdate(GameTime gameTime)
        {
            if ((player.MoveToDirect.X != 0 || player.MoveToDirect.Y != 0) && gameControl.AnimationLoop < 10)
            {
                if (gameControl.Animate >= 15)
                {
                    gameControl.Animate = 0;
                }
                gameControl.Animate += 1;
                delta.X += (int)(sizeTile * 0.1 * player.MoveToDirect.X);
                delta.Y += (int)(sizeTile * 0.1 * player.MoveToDirect.Y);
                gameControl.AnimationLoop += 1;
            }
            else
            {
                if (gameControl.AnimationLoop >= 10)
                {
                    gameState.CountSteps += 1;
                    player.Position = new Vector2(player.Position.X + (float)player.MoveToDirect.X, (float)player.Position.Y + ((float)player.MoveToDirect.Y));
                }
                gameControl.Animate = 0;
                delta = new Point(0, 0);
                gameControl.AnimationLoop = 0;
                player.MoveToDirect = new Point(0, 0);

                if (gameControl.DelayMove < 25 && Keyboard.GetState().IsKeyDown(gameControl.PreviousKey))
                {
                    gameControl.DelayMove += gameTime.ElapsedGameTime.Milliseconds;
                    return;
                }

                gameControl.PreviousKey = 0;
                gameControl.DelayMove = 0;
                var posit = new Point((int)player.Position.X, (int)player.Position.Y);

                ChangePosition(posit, gameTime);

            }
            player.Rectangle = new Rectangle((int)(player.Position.X * sizeTile * 2 + delta.X), (int)(player.Position.Y * sizeTile * 2 + delta.Y), sizeTile * 2, sizeTile * 2);

        }

        private void ChangePosition(Point posit, GameTime gameTime)
        {
            var temp = gameControl.MoveControl(gameControl.CheckPressKey(gameTime), posit, level);
            var field = level.Map[posit.Y + temp.Y, posit.X + temp.X];
            if ((temp.X != 0 || temp.Y != 0) && (field == (int)Cell.Box || field == (int)Cell.BoxOnPlace))
            {
                posit = new Point(posit.X + temp.X, posit.Y + temp.Y);
                temp = gameControl.MoveControl(temp, posit, level);
                field = level.Map[posit.Y + temp.Y, posit.X + temp.X];

                if ((temp.X != 0 || temp.Y != 0) && field != (int)Cell.Box && field != (int)Cell.BoxOnPlace)
                {
                    player.MoveToDirect = temp;
                    level.Map[posit.Y, posit.X] = level.Map[posit.Y, posit.X] == (int)Cell.BoxOnPlace ? (int)Cell.Place : (int)Cell.Ground;
                    level.Map[posit.Y + temp.Y, posit.X + temp.X] = field == (int)Cell.Place ? (int)Cell.BoxOnPlace : (int)Cell.Box;
                }
            }
            else if (field != (int)Cell.Box && field != (int)Cell.BoxOnPlace)
            {
                player.MoveToDirect = temp;
            }

            if (player.MoveToDirect.X != 0 || player.MoveToDirect.Y != 0)
            {
                ChangeFrame();
            }
        }

        private void ChangeFrame()
        {
            if (player.MoveToDirect.X == -1)
            {
                player.PlayerFrame = new Point(0, 2);
            }
            else if (player.MoveToDirect.X == 1)
            {
                player.PlayerFrame = new Point(0, 3);
            }
            else if (player.MoveToDirect.Y == -1)
            {
                player.PlayerFrame = new Point(0, 1);
            }
            else if (player.MoveToDirect.Y == 1)
            {
                player.PlayerFrame = new Point(0, 0);
            }
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (!gameState.IsGameStart || gameState.IsLvlComplete || gameState.IsGameOver)
            {
                scene.DrawScene(spriteBatch, gameState, level.Width * sizeTile, level.Heigth * sizeTile);
            }
            else
            {
                level.Draw(spriteBatch);
                player.Draw(spriteBatch, delta.X, delta.Y, gameControl.Animate);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}