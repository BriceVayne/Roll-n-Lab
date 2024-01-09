using System;

public class ModelValue<T> : IPropertyChanged<T>
{
    public Action<T> OnValueChanged { get; private set; }

    private T m_Value;
    public T Value
    {
        get => m_Value;
        set
        {
            if (!m_Value.Equals(value))
            {
                m_Value = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }

    public ModelValue() { }

    public ModelValue(T _Value)
        => m_Value = _Value;

    public void AddTriggerProperty(Action<T> _Method)
        => OnValueChanged += _Method;

    public void TriggerPropertyChanged()
        => OnValueChanged?.Invoke(m_Value);

    public void RemoveTriggerProperty(Action<T> _Method)
        => OnValueChanged -= _Method;

    public void RemoveAllTriggerProperty()
        => OnValueChanged = null;
}

public interface IPropertyChanged<T>
{
    public Action<T> OnValueChanged { get; }

    public void AddTriggerProperty(Action<T> _Method);
    public void RemoveTriggerProperty(Action<T> _Method);
    public void RemoveAllTriggerProperty();
    public void TriggerPropertyChanged();
}