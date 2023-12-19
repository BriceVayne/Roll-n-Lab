using Patterns;

namespace Managers
{
    public class GameManager : Singleton<GameManager>, IManager
    {
        public bool IsReady { get; private set; }

        private void Awake()
        {
            IsReady = false;
        }

        private void Start()
        {
            IsReady = true;
        }
    }
}