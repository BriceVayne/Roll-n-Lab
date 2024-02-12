using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    internal class LoadingTask
    {
        public string TaskName { get; private set; }
        public float MaxValue { get; private set; }
        public float Value { get; set; }

        public LoadingTask(string _TaskName, float _MaxValue)
        {
            TaskName = _TaskName;
            MaxValue = _MaxValue;
            Value = 0f;
        }
    }

    internal class LoadingService : Singleton<LoadingService>
    {
        public Action<string, float> UpdateTask;
        public Action<string> CompletedTask;

        private Queue<LoadingTask> m_LoadingTasks;

        public bool TryAddTask(string _TaskName, float _MaxValue)
        {
            if (string.IsNullOrEmpty(_TaskName))
                throw new NullReferenceException("Task name is null");

            if (m_LoadingTasks == null)
                m_LoadingTasks = new();

            var task = m_LoadingTasks.FirstOrDefault(i => i.TaskName == _TaskName);
            if (task == null)
            {
                m_LoadingTasks.Enqueue(new LoadingTask(_TaskName, _MaxValue));
                return true;
            }
            else
                return false;
        }

        private void Awake()
        {
            UpdateTask = null;
            CompletedTask = null;

            UpdateTask += OnUpdateTask;
            CompletedTask += OnCompletedTask;

            m_LoadingTasks = new();
        }

        private void OnUpdateTask(string _TaskName, float _Value)
        {
            if (string.IsNullOrEmpty(_TaskName))
                throw new NullReferenceException("Task name is null");

            var task = m_LoadingTasks.FirstOrDefault(i => i.TaskName == _TaskName);
            if (task != null)
                task.Value = _Value;
        }

        private void OnCompletedTask(string _TaskName)
        {
            if (string.IsNullOrEmpty(_TaskName))
                throw new NullReferenceException("Task name is null");

            if (m_LoadingTasks.Peek().TaskName == _TaskName)
                m_LoadingTasks.Dequeue();
        }
    }
}