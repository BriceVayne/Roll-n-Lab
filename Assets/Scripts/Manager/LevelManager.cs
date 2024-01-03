using Maze;
using Patterns;
using System;
using System.Collections.Generic;

namespace Managers
{
    public class LevelManager : Manager<LevelManager>
    {
        public Action OnGameWin;
        public Action OnGameOver;
        public Action OnGameReload;

        public Stack<TwoDimensionCell> SelectedPath { get; private set; }

        protected override void AwakeBehaviour()
        {
            OnGameWin = null;
            OnGameOver = null;
            OnGameReload = null;

            SelectedPath = new Stack<TwoDimensionCell>();
        }

        protected override void StartBehaviour()
        {
        }
    }
}
