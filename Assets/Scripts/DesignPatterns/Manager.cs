using UnityEngine;

namespace Patterns
{
    public abstract class Manager<T> : Singleton<T> where T : MonoBehaviour
    {
        public bool IsReady { get; private set; }

        protected abstract void AwakeBehaviour();

        protected abstract void StartBehaviour();

        private void Awake()
        {
            IsReady = false;

            AwakeBehaviour();
        }

        private void Start()
        {
            StartBehaviour();

            IsReady = true;
        }
    }
}
