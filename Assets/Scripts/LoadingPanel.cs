using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    internal class LoadingPanel : MonoBehaviour
    {
        [SerializeField][Range(1, 10)] private int m_NumberOfSquare = 1;
        [SerializeField] private Image m_Prefab;
        [SerializeField] private Transform m_Content;
        [SerializeField] private List<Color> m_ColorPalette;

        private const float MAX_TIME = .25f;

        private List<Image> m_Squares;
        private float m_Time;
        private int m_ColorIndex;
        private int m_PaletteCount;

        private void Start()
        {
            m_Squares = new();
            m_Time = 0;
            m_ColorIndex = 0;

            m_PaletteCount = m_ColorPalette.Count;
            if (m_PaletteCount < 2)
                throw new NullReferenceException("Add at least 2 colors to the palette.");

            for (int i = 0; i < m_NumberOfSquare; i++)
            {
                Image img = Instantiate(m_Prefab, m_Content);
                img.color = Color.white;
                m_Squares.Add(img);
            }
        }

        private void Update()
        {
            m_Time += Time.deltaTime;
            if (m_Time < MAX_TIME)
            {
                for (int i = 0; i < m_Squares.Count; i++)
                    m_Squares[i].color = Color.Lerp(m_ColorPalette[(m_ColorIndex+i) % m_PaletteCount], 
                                                    m_ColorPalette[((m_ColorIndex + 1) + i) % m_PaletteCount], 
                                                    m_Time);
            }
            else
            {
                m_ColorIndex++;
                m_Time = 0;
            }
        }
    }
}