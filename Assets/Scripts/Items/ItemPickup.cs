using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 9f;
    [SerializeField] private ItemEncounterTrigger encounterTrigger;
    [SerializeField] private TextMeshProUGUI interactionText;
    private bool playerNearby = false;
    private bool isCollected;
    private Transform player;
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


    private void Start()
    {
        interactionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        bool playerNearby = distance <= interactionRadius;

        interactionText.gameObject.SetActive(playerNearby);

        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
        }
    }
}
