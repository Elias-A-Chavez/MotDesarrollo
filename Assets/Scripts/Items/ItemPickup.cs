using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemEncounterTrigger encounterTrigger;

    private bool isCollected;

    public void CollectItem()
    {
        if (isCollected)
        {
            return;
        }

        isCollected = true;

        if (encounterTrigger != null)
        {
            encounterTrigger.StartEncounter();
        }

        gameObject.SetActive(false);
    }
}
