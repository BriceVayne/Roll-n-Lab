using Patterns;
using System;

namespace Services
{
    internal sealed class LevelService : Singleton<LevelService>
    {
        public Action OnLevelFinished;
        public Action OnLevelOver;
        public Action OnLevelReload;

        private void Awake()
        {
            OnLevelFinished = null;
            OnLevelOver = null;
            OnLevelReload = null;
        }
    }
}
