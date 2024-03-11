using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Framework
{
    public struct SStopwatchOther
    {
        public long Milliseconds;
        public long Ticks;
        public TimeSpan Time;
    }

    public class StopWatch
    {
        private Stopwatch m_Global;
        private Dictionary<string, Stopwatch> m_Others;
        private SStopwatchOther m_TotalOthers;

        public StopWatch()
        {
            m_Global = new Stopwatch();
            m_TotalOthers = new SStopwatchOther();
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

            m_TotalOthers.Milliseconds += m_Others[methodName].ElapsedMilliseconds;
            m_TotalOthers.Ticks += m_Others[methodName].ElapsedTicks;
            m_TotalOthers.Time += m_Others[methodName].Elapsed;
        }

        public void ResetFromMethod([CallerMemberName] string methodName = null)
        {
            if (string.IsNullOrEmpty(methodName) || !m_Others.ContainsKey(methodName))
                return;

            m_TotalOthers.Milliseconds -= m_Others[methodName].ElapsedMilliseconds;
            m_TotalOthers.Ticks -= m_Others[methodName].ElapsedTicks;
            m_TotalOthers.Time -= m_Others[methodName].Elapsed;

            m_Others[methodName].Reset();

        }

        public void Clear()
        {
            m_Others.Clear();
            m_TotalOthers = new SStopwatchOther();
        }
    }
}