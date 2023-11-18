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

        public static Color GetColorByType(ECellType _ECellType)
        {
            Color color;
            switch (_ECellType)
            {
                case ECellType.BORDER:
                    color = Color.black;
                    break;
                case ECellType.WALL:
                    color = Color.black;
                    break;
                case ECellType.START:
                    color = Color.green;
                    break;
                case ECellType.END:
                    color = Color.red;
                    break;
                case ECellType.EMPTY:
                    color = Color.white;
                    break;
                case ECellType.PATH:
                    color = Color.white;
                    break;
                default:
                    color = Color.white;
                    break;
            }

            return color;
        }
    }
}
