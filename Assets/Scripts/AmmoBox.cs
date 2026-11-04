using UnityEngine;
using TMPro;
public class AmmoBox : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int ammoAmount = 10;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [Header("UI Reference")]
    [SerializeField] private GameObject interactionTextUI;
    private bool isPlayerNearby = false;
    private PlayerInventory playerInventory;
    private void Update()
    {

        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            TryPickUpAmmo();
        }
    }
    private void TryPickUpAmmo()
    {
        if (playerInventory != null)
        {

            bool wasPickedUp = playerInventory.AddAmmo(ammoAmount);
            if (wasPickedUp)
            {
                if (interactionTextUI != null)
                {
                    interactionTextUI.SetActive(false);
                }
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            isPlayerNearby = true;
            playerInventory = inventory;

            
            if (interactionTextUI != null)
            {
                interactionTextUI.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() == playerInventory)
        {
            isPlayerNearby = false;
            playerInventory = null;
            
            if (interactionTextUI != null)
            {
                interactionTextUI.SetActive(false);
            }
        }
    }
}



