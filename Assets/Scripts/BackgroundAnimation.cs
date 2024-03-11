using Framework;
using Generator;
using Maze;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    internal sealed class BackgroundAnimation : MonoBehaviour
    {
        [SerializeField][Range(1, 10)] private int m_SizeMultiplier;
        [SerializeField] private Transform m_Content;
        [SerializeField] private CellView m_Prefab;
        [SerializeField] private float m_SpeedInSeconds;

        private Camera m_Camera;
        private GridGenerator m_GridGenerator;
        private StopWatch m_StopWatch;
        private CellView[,] m_Grid;
        private Queue<CellModel[,]> m_Iterations;
        private Queue<CellModel[,]> m_NextIterations;
        private Vector2Int m_Size;
        private float m_ElapsedTime;
        private bool m_HasGenerateNextIteration;
        private Coroutine m_Coroutine;

        private void Awake()
        {
            m_Size = GetSizeFromScreen();
            m_Camera = Camera.main;
            m_GridGenerator = new GridGenerator(m_Size);
            m_StopWatch = new StopWatch();
            m_Grid = new CellView[m_Size.x, m_Size.y];
            m_ElapsedTime = 0f;
            m_HasGenerateNextIteration = false;
        }

        private void Start()
        {
            SetCameraPosition();

            m_Iterations = m_GridGenerator.Regenerate();

            SpawnGrid();
        }

        private void Update()
        {
            if (!m_HasGenerateNextIteration)
            {
                m_HasGenerateNextIteration = true;
                if (m_Coroutine != null)
                    StopCoroutine(m_Coroutine);

                m_Coroutine = StartCoroutine(LoadNextIteration());
            }

            m_ElapsedTime += Time.deltaTime;
            if (m_ElapsedTime >= m_SpeedInSeconds)
            {
                if (m_Iterations.TryDequeue(out CellModel[,] iteration))
                {
                    UpdateGrid(iteration);
                    m_ElapsedTime = 0;
                }
                else
                {
                    m_Iterations = new Queue<CellModel[,]>(m_NextIterations);
                    m_HasGenerateNextIteration = false;
                }
            }
        }

        private Vector2Int GetSizeFromScreen()
        {
            Debug.Log($"Screen size: {Screen.width}x{Screen.height}");
            Debug.Log($"Screen Safe Area size: {Screen.safeArea.width}x{Screen.safeArea.height}");

            var width = Mathf.FloorToInt(Screen.safeArea.width / 100) * m_SizeMultiplier;
            var height = Mathf.FloorToInt(Screen.safeArea.height / 100) * m_SizeMultiplier;

            if (width % 2 == 0)
                width++;

            if (height % 2 == 0)
                height++;

            return new Vector2Int(width, height);
        }

        private void SetCameraPosition()
        {
            var width = Mathf.FloorToInt(Screen.width / 100) * m_SizeMultiplier;
            var height = Mathf.FloorToInt(Screen.height / 100) * m_SizeMultiplier;

            var x = width % 2 != 0 ? (width - 1) / 2f : width / 2f;
            var y = height % 2 != 0 ? Mathf.Floor((height - 1) / 2f) : Mathf.Floor(height / 2f);

            m_Camera.transform.position = new Vector3(x, y, -5f);
            m_Camera.orthographicSize = (height / 2f) + 1f;
        }

        private void SpawnGrid()
        {
            m_StopWatch.StartFromMethod();

            if (m_Prefab == null || m_Content == null)
                return;

            m_Grid = new CellView[m_Size.x, m_Size.y];

            for (int x = 0; x < m_Size.x; x++)
                for (int y = 0; y < m_Size.y; y++)
                {
                    var cell = Instantiate(m_Prefab, m_Content);
                    cell.name = $"Cell [{x},{y}]";
                    cell.transform.position = new Vector3(x, y, 0);
                    m_Grid[x, y] = cell;
                }

            m_StopWatch.StopFromMethod();
        }

        private void UpdateGrid(CellModel[,] _Iteration)
        {
            for (int x = 0; x < m_Size.x; x++)
                for (int y = 0; y < m_Size.y; y++)
                    m_Grid[x, y].SetColor(_Iteration[x, y].Value, m_SpeedInSeconds);
        }

        private IEnumerator LoadNextIteration()
        {
            yield return null;
            m_NextIterations = m_GridGenerator.Regenerate();
        }


    }
}
