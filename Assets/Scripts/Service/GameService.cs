using Patterns;
using UnityEngine;

namespace Services
{
    internal sealed class GameService : Singleton<GameService>
    {
        public EGameMode Mode {  get { return m_GameMode; } private set { m_GameMode = value; } }

        [SerializeField] private EGameMode m_GameMode;


    }
}