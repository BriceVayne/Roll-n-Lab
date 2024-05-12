using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Game.Framework
{
    internal sealed class StopWatch
    {
        private Stopwatch m_Global;
        private Dictionary<string, Stopwatch> m_Others;

        public StopWatch()
        {
            m_Global = new Stopwatch();
            m_Others = new Dictionary<string, Stopwatch>();
        }

        public void Start()
            => m_Global.Start();

        public void Stop()
            => m_Global.Stop();

        public void Reset()
            => m_Global.Reset();


        public void StartFromMethod([CallerMemberName] string methodName = null)
        {
            if (string.IsNullOrEmpty(methodName))
                return;

            if (!m_Others.ContainsKey(methodName))
                m_Others.Add(methodName, new Stopwatch());

            m_Others[methodName].Start();
        }

        public void StopFromMethod([CallerMemberName] string methodName = null)
        {
            if (string.IsNullOrEmpty(methodName) || !m_Others.ContainsKey(methodName))
                return;

            m_Others[methodName].Stop();
        }

        public void ResetFromMethod([CallerMemberName] string methodName = null)
        {
            if (string.IsNullOrEmpty(methodName) || !m_Others.ContainsKey(methodName))
                return;

            m_Others[methodName].Reset();
        }

        public string GetTimeFromMethod([CallerMemberName] string methodName = null)
        {
            if (string.IsNullOrEmpty(methodName) || !m_Others.ContainsKey(methodName))
                return null;

            string format = "{0,-32} | {1,-10} | {2,-5}";
            return string.Format(format, methodName, m_Others[methodName].ElapsedTicks, m_Others[methodName].ElapsedMilliseconds);
        }

        public void Clear()
        {
            m_Global.Reset();
            m_Others.Clear();
        }
    }
}