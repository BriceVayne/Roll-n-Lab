using Managers;
using TMPro;
using UnityEngine;

namespace UserInterfaces
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_WinText;
        [SerializeField] private TMP_Text m_InstructionText;

        private void Start()
        {
            LevelManager.Instance.OnGameWin += EnableWinHUD;
            LevelManager.Instance.OnGameOver += DisableWinHUD;
            LevelManager.Instance.OnGameReload += DisableWinHUD;

            DisableWinHUD();
        }

        private void EnableWinHUD()
        {
            m_WinText.gameObject.SetActive(true);
            m_InstructionText.gameObject.SetActive(true);
        }

        private void DisableWinHUD()
        {
            m_WinText.gameObject.SetActive(false);
            m_InstructionText.gameObject.SetActive(false);
        }
    }
}