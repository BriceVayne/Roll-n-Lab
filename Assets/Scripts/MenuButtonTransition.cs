using Orchestrator;
using UnityEngine;

internal sealed class MenuButtonTransition : MonoBehaviour
{
    public void LoadSoloScene()
         => SceneOrchestrator.Instance.LoadScenes.Invoke(ESceneOrder.LEVEL);

    public void LoadMultiScene() { }

    public void LoadOptionScene() { }

    public void Quit()
        => Application.Quit();
}
