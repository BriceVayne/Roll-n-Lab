using Game.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
internal struct S_Range
{
    public Vector2Int Range;
    public bool IsActive;
}

internal sealed class GridGeneratorBenchmark : MonoBehaviour
{
    [SerializeField] private List<S_Range> m_BenchmarkSize;

    private GridGeneratorJob gridGen;

    private void Start()
    {
        foreach (var size in m_BenchmarkSize)
        {
            if (size.IsActive)
                gridGen = new GridGeneratorJob(size.Range);
        }
    }
}
