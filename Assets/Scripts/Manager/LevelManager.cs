using Maze;
using Patterns;
using System;
using System.Collections.Generic;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Action OnGameWin;
        public Action OnGameOver;
        public Action OnGameReload;

        public Stack<TwoDimensionCell> SelectedPath { get; private set; }

        private void Awake()
        {
            OnGameWin = null;
            OnGameOver = null;
            OnGameReload = null;

            SelectedPath = new Stack<TwoDimensionCell>();
        }

        private void Start()
        {
        }
    }
}
