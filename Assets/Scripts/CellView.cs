using Framework;
using UnityEngine;

namespace Maze
{
    internal sealed class CellView : MonoBehaviour
    {
        private SpriteRenderer m_SpriteRenderer;
        private Color m_StartColor;
        private Color m_EndColor;
        private float m_FadeDuration;
        private float m_ElapsedTime;
        private int m_PreviousValue;
        private bool m_Running;

        private void Awake()
        {
            if (TryGetComponent(out SpriteRenderer image))
                m_SpriteRenderer = image;
        }

        private void Update()
        {
            if (!m_Running)
                return;

            if (m_FadeDuration < 1)
            {
                m_SpriteRenderer.color = m_EndColor;
                m_Running = false;
            }
            else
            {
                m_ElapsedTime += Time.deltaTime;
                if (m_ElapsedTime >= m_FadeDuration)
                {
                    m_SpriteRenderer.color = Color.Lerp(m_StartColor, m_EndColor, m_ElapsedTime);
                    m_Running = false;
                }
            }
        }

        public void SetColor(int _Value, float _Time)
        {
            if (m_PreviousValue == _Value)
                return;

            m_StartColor = m_SpriteRenderer.color;
            m_EndColor = ColorGenerator.GetColor(_Value);
            m_FadeDuration = _Time;
            m_ElapsedTime = 0f;
            m_PreviousValue = _Value;
            m_Running = true;
        }
    }
}
