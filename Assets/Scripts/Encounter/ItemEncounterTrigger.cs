using UnityEngine;

public class ItemEncounterTrigger : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    private bool encounterTriggered;

    public void StartEncounter()
    {
        if (encounterTriggered)
        {
            return;
        }

        if (waveManager == null)
        {
            Debug.LogError("WaveManager is not assigned.");
            return;
        }

        encounterTriggered = true;
        waveManager.StartEncounter();
    }
}
