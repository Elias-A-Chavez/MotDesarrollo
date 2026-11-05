using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeButtonController : MonoBehaviour
{
    [SerializeField] private Button escapeButton;
    [SerializeField] private UnityEvent escapeAction;

    private bool bossDefeated;

    private void Start()
    {
        if (escapeButton != null)
        {
            escapeButton.interactable = false;
        }
    }

    public void NotifyBossDefeated()
    {
        if (bossDefeated)
        {
            return;
        }

        bossDefeated = true;

        if (escapeButton != null)
        {
            escapeButton.interactable = true;
        }
    }

    public void Escape()
    {
        if (!bossDefeated)
        {
            return;
        }

        SceneManager.LoadScene("Victory");
    }
}