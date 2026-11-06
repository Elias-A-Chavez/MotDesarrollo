using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 6f;
    [SerializeField] private ItemEncounterTrigger encounterTrigger;
    [SerializeField] private TextMeshProUGUI interactionText;

    private bool playerNearby = false;
    private bool isCollected = false;

    private Transform player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (player == null || isCollected)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        playerNearby = distance <= interactionRadius;

        interactionText.gameObject.SetActive(playerNearby);

        if (playerNearby && Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            CollectItem();
        }
    }

    public void CollectItem()
    {
        if (isCollected)
            return;

        isCollected = true;

        encounterTrigger.StartEncounter();

        gameObject.SetActive(false);
    }
}