using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionRadius = 6f;
    [SerializeField] private ItemEncounterTrigger encounterTrigger;
    [SerializeField] private TextMeshProUGUI interactionText;

    private bool playerNearby;
    private bool isCollected;

    private Transform player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        SetInteractionTextVisible(false);
    }

    private void Update()
    {
        if (player == null || isCollected)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        playerNearby = distance <= interactionRadius;

        SetInteractionTextVisible(playerNearby);

        if (playerNearby && Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    public void Interact()
    {
        CollectItem();
    }

    public void CollectItem()
    {
        if (isCollected)
        {
            return;
        }

        isCollected = true;

        if (player != null)
        {
            Weapon weapon = player.GetComponentInChildren<Weapon>(true);

            if (weapon != null && weapon.transform.parent != null)
            {
                weapon.transform.parent.gameObject.SetActive(true);
            }
        }

        SetInteractionTextVisible(false);

        if (encounterTrigger != null)
        {
            encounterTrigger.StartEncounter();
        }

        gameObject.SetActive(false);
    }

    private void SetInteractionTextVisible(bool visible)
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(visible);
        }
    }
}
