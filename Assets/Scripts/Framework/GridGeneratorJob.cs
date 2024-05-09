using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Framework
{
    internal struct S_Cell
    {

    }

    internal sealed class GridGeneratorJob
    {
        NativeArray<S_Cell> m_Grid;

        public GridGeneratorJob(Vector2Int _Size)
        {
            m_Grid = new NativeArray<S_Cell>(_Size.x * _Size.y, Allocator.Persistent);

        }

    }
}
