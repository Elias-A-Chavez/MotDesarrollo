using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private BossActivation bossActivation;

    private bool isOpen;

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

        if (bossActivation != null)
        {
            bossActivation.ActivateBoss();
        }
    }
}
