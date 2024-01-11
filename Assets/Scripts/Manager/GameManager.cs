using Patterns;
using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameMode m_GameMode;
        public GameMode Mode {  get { return m_GameMode; } private set { m_GameMode = value; } }
    }

    [Serializable]
    public enum GameMode
    {
        GAME_EDITOR,
        GAME_RUNTIME,
        DEBUG_EDITOR,
        DEBUG_RUNTIME,
    }
}