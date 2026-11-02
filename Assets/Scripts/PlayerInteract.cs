using Mono.Cecil;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        IInteractable iinteract = collision.gameObject.GetComponent<IInteractable>();
        {
            if (iinteract != null)
            {
                iinteract.Interact();
            }
        }
    }
}
