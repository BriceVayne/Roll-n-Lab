using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public static class CellColorGenerator
    {
        private static Dictionary<int, Color> m_CellColor;

        public static Color GetColor(int _Value)
        {
            if (m_CellColor == null)
                m_CellColor = new Dictionary<int, Color>();

            if (!m_CellColor.ContainsKey(_Value))
            {
                Color color;
                if(_Value == 0)
                    color = Color.white;
                else if (_Value < 0)
                    color = Color.black;
                else if(_Value > 0)
                    color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f);
                else
                    color = Color.gray;

                m_CellColor.Add(_Value, color);
            }

            return m_CellColor[_Value];
        }
    }
}
