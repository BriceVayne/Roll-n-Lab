using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public abstract class AbstractLogger
    {
        public abstract void Log(string filterName, string message);
        public abstract void LogWarning(string filterName, string message);
        public abstract void LogError(string filterName, string message);
        public abstract void LogAssert(bool condition, string filterName, string message);
    }

    internal class LogService : Singleton<LogService>
    {
        public void Log(string message) { }

        public void LogWarning(string message) { }

        public void LogError(string message) { }

    }
}