using System;

namespace Services
{
    [Serializable]
    public enum EGameMode
    {
        GAME_EDITOR,
        GAME_RUNTIME,
        DEBUG_EDITOR,
        DEBUG_RUNTIME,
    }
}
