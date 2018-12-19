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
    class Scenes
    {
        private Texture2D start;
        private SpriteFont scoreFont;
        private SpriteFont stateFont;

        public Texture2D Start
        {
            get
            { return start; }
            set
            { start = value; }
        }

        public SpriteFont ScoreFont
        {
            get
            { return scoreFont; }
            set
            { scoreFont = value; }
        }

        public SpriteFont StateFont
        {
            get
            { return stateFont; }
            set
            { stateFont = value; }
        }

        public void Load(ContentManager Content)
        {
            Start = Content.Load<Texture2D>("Start");
            ScoreFont = Content.Load<SpriteFont>("Score");
            StateFont = Content.Load<SpriteFont>("GameState");
        }

        public void DrawScene(SpriteBatch spriteBatch, GameState gameState, int width, int heigth)
        {
            spriteBatch.Draw(this.Start, new Rectangle(0, 0, width, heigth), Color.White);
            if (!gameState.IsGameStart)
            {
                spriteBatch.DrawString(this.StateFont, "S_o_K_o_B_a_N", new Vector2(25, heigth / 3), Color.DarkRed);
                spriteBatch.DrawString(this.StateFont, "Enter", new Vector2(165, heigth / 2), Color.White);
            }
            else if (gameState.IsGameOver)
            {
                spriteBatch.DrawString(this.StateFont, "GameOver", new Vector2(80, heigth / 3), Color.DarkRed);
                spriteBatch.DrawString(this.StateFont, "Esc", new Vector2(165, heigth / 2), Color.White);
            }
            else
            {
                spriteBatch.DrawString(this.StateFont, "Steps: " + gameState.CountSteps, new Vector2(80, heigth / 3), Color.DarkRed);
                spriteBatch.DrawString(this.StateFont, "Enter", new Vector2(165, heigth / 2), Color.White);
            }
        }
    }
}
