using Maze;
using Patterns;
using System;
using System.Collections.Generic;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>, IManager
    {
        public static Action OnGameWin;
        public static Action OnGameOver;
        public static Action OnGameReload;

        public bool IsReady { get; private set; }
        public static Stack<TwoDimensionCell> SelectedPath { get; private set; }

        private void Awake()
        {
            IsReady = false;

            OnGameWin = null;
            OnGameOver = null;
            OnGameReload = null;

            SelectedPath = new Stack<TwoDimensionCell>();
        }

        private void Start()
        {
            IsReady = true;
        }
    }
}
