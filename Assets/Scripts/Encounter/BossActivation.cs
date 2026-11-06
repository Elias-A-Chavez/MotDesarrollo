using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private int bossStartingHealth = 100;
    [SerializeField] private EscapeButtonController escapeButtonController;

    private bool bossActivated;

    private void Start()
    {
        if (boss != null)
        {
            boss.SetActive(false);
        }
    }

    public void ActivateBoss()
    {
        if (bossActivated || boss == null)
        {
            return;
        }

        EnemyHealth bossHealth = boss.GetComponent<EnemyHealth>();

        if (bossHealth == null)
        {
            Debug.LogError("Boss does not contain EnemyHealth.");
            return;
        }

        if (escapeButtonController == null)
        {
            Debug.LogError("EscapeButtonController is not assigned.");
            return;
        }

        bossHealth.InitializeBoss(
            bossStartingHealth,
            escapeButtonController
        );

        bossActivated = true;
        boss.SetActive(true);
    }
}   