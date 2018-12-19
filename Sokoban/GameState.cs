using Microsoft.Xna.Framework;
using Sokoban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class GameState
    {
        private int lvlNumber;
        private bool isLvlComplete;
        private bool isGameStart;
        private bool isGameOver;
        private int countSteps;

        public int LvlNumber
        {
            get
            { return lvlNumber; }
            set
            { lvlNumber = value; }
        }

        public bool IsLvlComplete
        {
            get
            { return isLvlComplete; }
            set
            { isLvlComplete = value; }
        }

        public bool IsGameStart
        {
            get
            { return isGameStart; }
            set
            { isGameStart = value; }
        }

        public bool IsGameOver
        {
            get
            { return isGameOver; }
            set
            { isGameOver = value; }
        }

        public int CountSteps
        {
            get
            { return countSteps; }
            set
            { countSteps = value; }
        }

        public GameState()
        {
            lvlNumber = 0;
            isLvlComplete = false;
            countSteps = 0;
        }

        public bool IsLevelComplete(Level level)
        {
            var isComplete = true;
            for (int i = 0; i < level.Map.GetLength(0); i++)
            {
                for (int j = 0; j < level.Map.GetLength(1); j++)
                {
                    if (level.Map[i, j] == 3)
                        isComplete = false;
                }
            }

            return isComplete;
        }
    }
}
