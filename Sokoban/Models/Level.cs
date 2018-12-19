using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models
{
    class Level
    {
        const int sizeTile = 32;
        private List<TilesLoader> allTiles = new List<TilesLoader>();
        private int width;
        private int heigth;
        private int[,] map;

        public List<TilesLoader> AllTiles => allTiles;

        public int Width
        {
            get
            { return width; }
            set
            { width = value; }
        }

        public int Heigth
        {
            get
            { return heigth; }
            set
            { heigth = value; }
        }

        public int[,] Map
        {
            get
            { return map; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                map = value;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            BuildMap();
            foreach (var tile in AllTiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public void BuildMap()
        {
            AllTiles.Clear();
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    if (map[j, i] > 0)
                    {
                        AllTiles.Add(new TilesLoader(map[j, i], new Rectangle(i * sizeTile, j * sizeTile, sizeTile, sizeTile)));
                    }
                }
            }
        }
    }
}

