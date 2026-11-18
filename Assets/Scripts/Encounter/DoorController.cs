using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private BossActivation bossActivation;

    private EnemyHealth bossHealth;
    private bool isOpen;

    private void Start()
    {
        if (bossActivation == null)
        {
            return;
        }

        bossHealth = bossActivation.BossHealth;

        if (bossHealth != null)
        {
            bossHealth.Defeated += OpenDoor;
        }
    }

    private void OnDestroy()
    {
        if (bossHealth != null)
        {
            bossHealth.Defeated -= OpenDoor;
        }
    }

    public void StartBossFight()
    {
        if (bossActivation != null)
        {
            bossActivation.ActivateBoss();
        }
    }

    public void OpenDoor()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;

        if (door != null)
        {
            door.SetActive(false);
        }
    }
}
