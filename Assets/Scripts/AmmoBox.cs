using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int ammoAmount = 10;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
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
            Debug.Log("Press '" + interactKey + "' to pick up ammo.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.GetComponent<PlayerInventory>() == playerInventory)
        {
            isPlayerNearby = false;
            playerInventory = null;
            Debug.Log("Player walked away from ammo box.");
        }
    }
}
