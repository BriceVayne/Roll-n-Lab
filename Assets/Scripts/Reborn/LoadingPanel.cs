using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    internal class LoadingPanel : MonoBehaviour
    {
        [SerializeField] private Image filledImage;
        [SerializeField] private TMP_Text filledText;

        private void Start()
        {
            LoadingService.Instance.UpdateTask += OnUpdateTask;
            LoadingService.Instance.CompletedTask += OnCompletedTask;
        }

        private void OnUpdateTask(string _TaskName, float _Value)
        {
            if (filledImage != null)
                filledImage.fillAmount += _Value;

            if (filledText != null)
                filledText.text = $"{_TaskName}: {(int)_Value}%";
        }

        private void OnCompletedTask(string _TaskName)
        {
            if (filledImage != null)
                filledImage.fillAmount += 100f;

            if (filledText != null)
                filledText.text = $"{_TaskName}: 100%";
        }
    }
}