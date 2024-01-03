using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace Framework
{
    public abstract class PropertyChange<TModel>
    {
        private Dictionary<string, Dictionary<int, Action<object>>> m_PropertiesCallback;

        public void AddPropertyCallback<T>(string _PropertyName, Object _RefObject, Action<T> _Callback)
        {
            if (m_PropertiesCallback == null)
                m_PropertiesCallback = new Dictionary<string, Dictionary<int, Action<object>>>();

            int refObject = _RefObject.GetInstanceID();

            if (m_PropertiesCallback.ContainsKey(_PropertyName))
            {
                if (m_PropertiesCallback[_PropertyName].ContainsKey(refObject))
                    m_PropertiesCallback[_PropertyName][refObject] += (action) => _Callback((T)action);
                else
                    m_PropertiesCallback[_PropertyName].Add(refObject, (action) => _Callback((T)action));
            }
            else
                m_PropertiesCallback.Add(_PropertyName, new Dictionary<int, Action<object>>() { { refObject, (action) => _Callback((T)action) } });
        }

        public void AddPropertyCallback<T>(Expression<Func<TModel, T>> _Member, Object _RefObject, Action<T> _Callback)
            => AddPropertyCallback(_Member.ExpressionToString(), _RefObject, _Callback);

        public void RemovePropertiesCallbacks(Object _RefObject)
        {
            int refObject = _RefObject.GetInstanceID();
            foreach (var property in m_PropertiesCallback)
            {
                if (property.Value.ContainsKey(refObject))
                    property.Value.Remove(refObject);
            }
        }

        public void TriggerPropertiesCallbacks(Object _RefObject)
        {
            if (_RefObject == null)
                return;

            int refObject = _RefObject.GetInstanceID();

            foreach (var property in m_PropertiesCallback)
            {
                if (property.Value.ContainsKey(refObject))
                {
                    foreach (var callback in property.Value.Values)
                        callback.Invoke(GetType().GetProperty(property.Key).GetValue(this));
                }
            }
        }

        protected bool SetField<T>(ref T _Field, T _Value, [CallerMemberName] string _PropertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(_Field, _Value))
                return false;

            _Field = _Value;
            if (m_PropertiesCallback.ContainsKey(_PropertyName))
                for (int i = m_PropertiesCallback[_PropertyName].Count - 1; i >= 0; i--)
                    m_PropertiesCallback[_PropertyName]?.ElementAt(i).Value?.Invoke(_Value);

            return true;
        }
    }
}