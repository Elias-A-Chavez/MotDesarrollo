using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [SerializeField] private GameObject boss;

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

        bossActivated = true;
        boss.SetActive(true);
    }
}
