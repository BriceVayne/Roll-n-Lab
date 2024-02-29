using Reborn.Menu;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PreSceneLoader : MonoBehaviour
{
    [SerializeField] private List<SceneAsset> m_SceneAsset;

    private void Awake()
    {
        if (m_SceneAsset == null)
            throw new NullReferenceException("No scene to pre-load");

        Debug.Log("Pre Loading [START]");

        foreach (var asset in m_SceneAsset)
            SceneLoaderService.Instance.LoadSceneAdditive(asset);

        Debug.Log("Pre Loading [END]");

        Destroy(gameObject);
    }
}
