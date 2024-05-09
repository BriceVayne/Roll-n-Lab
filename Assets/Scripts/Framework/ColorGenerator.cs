using Maze;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Framework
{
    internal static class ColorGenerator
    {
        private static SortedDictionary<int, Color> m_CellColor = new SortedDictionary<int, Color>();

        public static Color GetColor(int _Value)
        {
            if (!m_CellColor.ContainsKey(_Value))
                GenerateColor(_Value);

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
                    color = Color.gray;
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
                    color = Color.cyan;
                    break;
                default:
                    color = Color.black;
                    break;
            }

            return color;
        }

        private static void GenerateColor(int _Value)
        {
            Color color;
            if (_Value == 0)
                color = Color.white;
            else if (_Value < 0)
                color = Color.black;
            else if (_Value > 0)
                color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f);
            else
                color = Color.black;

            m_CellColor.Add(_Value, color);
        }
    }
}